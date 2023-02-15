using System.Security.Claims;
using AutoMapper;
using Azure;
using Azure.Core;
using GuacAPI.Authorization;
using GuacAPI.Context;
using GuacAPI.Entities;
using GuacAPI.Helpers;
using GuacAPI.Models;
using GuacAPI.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GuacAPI.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UserService(IHttpContextAccessor httpContextAccessor, DataContext context,
        IJwtUtils jwtUtils, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    // pas mettre de variable dans la function, mais utiliser le _httpContextAccessor


    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public Task<User> GetUserById(int id)
    {
        GetUser(id);
        var userId = _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return userId;
    }


    //todo verifier si email deja existant

    public async Task<User> GetUserByUsername(string username)
    {
        
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }


    public async Task<User> UpdateUser(User request, int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        user.Username = request.Username;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public void Register(RegisterRequest request)
    {
        if (_context.Users.Any(x => x.Username == request.Username) ||
            _context.Users.Any(x => x.Email == request.Email))
            throw new AppException("Username '" + request.Username + "' is already taken" +
                                   " or email '" + request.Email + "' is already taken");

        var register = _mapper.Map<User>(request);
        register.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _context.Users.Add(register);
        _context.SaveChanges();
    }

    public AuthenticateResponse Login(AuthenticateRequest model, string ipAddress)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
        
        if (user == null || BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash) == false)
            throw new AppException("Username or password is incorrect");
        var jwtToken = _jwtUtils.GenerateToken(user);
        var tokenBdd = _jwtUtils.GenerateToken(ipAddress);
        // si y a deja un token, le supprimer et en creer un nouveau 
        if (user.RefreshTokens != null)
        {
             // trouve moi les tokens qui sont actifs
            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.TokenExpires > DateTime.UtcNow || x.newTokenExpires > DateTime.UtcNow);
            if (refreshToken != null)
            {
                // supprime les tokens qui sont inactifs
                user.RefreshTokens.Remove(refreshToken);
                _context.Remove(refreshToken);
            }

        }
       user.RefreshTokens.Add(tokenBdd);
        _context.Update(tokenBdd);
        _context.SaveChanges();
        if (tokenBdd.Token != null) return new AuthenticateResponse(user, jwtToken, tokenBdd.Token, tokenBdd.TokenExpires);
        return new AuthenticateResponse(user, jwtToken, tokenBdd.newToken, tokenBdd.newTokenExpires);
    }

    public AuthenticateResponse RefreshToken(string token, int id)
    {
        // mais user peut etre null

       var user =GetById(id);
    if (user == null)
    {
        return null; // or throw an exception, depending on your needs
    }

    var tokenUser = user.RefreshTokens?.Find(x => x.UserId == id);
    if (tokenUser == null)
    {
        return null; // or throw an exception, depending on your needs
    }
        var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
        if(refreshToken != null) {
            user.RefreshTokens.Remove(refreshToken);
            _context.Remove(refreshToken);
        }
            refreshToken.newToken = _jwtUtils.GenerateRefreshToken(token).newToken;
            refreshToken.newTokenExpires = DateTime.UtcNow.AddDays(7);
            user.RefreshTokens.Add(refreshToken);
            _context.Update(refreshToken);
            _context.SaveChanges();
            var jwtToken = _jwtUtils.GenerateToken(user);
            return new AuthenticateResponse(user, jwtToken, refreshToken.Token,refreshToken.TokenExpires);
        }

    
    public void ResetPassword(ResetPasswordRequest model)
    {
        var user = GetUserByEmail(model.Email);
        // validate
        
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = GetUser(id);

        // validate
        if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    
    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public User GetUserByRefreshToken(string token)
    {
        var user = _context.Users.SingleOrDefault(u =>
            u.RefreshTokens != null && u.RefreshTokens.Any(t => t.Token == token));

        if (user == null)
            throw new AppException("Invalid token");
        return user;
    }

    private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        if (newRefreshToken is null)
        {
            throw new Exception("token is null");
        }

        newRefreshToken.Token = refreshToken.Token;
        return newRefreshToken;
    }


    private void RemoveOldRefreshTokens(User user)
    {
        // remove nez refresh tokens that have expired
       // tu supprimes le token le plus ancien 
        user.RefreshTokens.Remove(user.RefreshTokens.OrderBy(x => x.TokenExpires).First());
       
    }


    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}