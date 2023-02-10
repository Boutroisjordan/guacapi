using System.Security.Claims;
using AutoMapper;
using GuacAPI.Authorization;
using GuacAPI.Context;
using GuacAPI.Entities;
using GuacAPI.Helpers;
using GuacAPI.Models;
using GuacAPI.Models.Users;
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
        user.RefreshTokens.Add(tokenBdd);
        RemoveOldRefreshTokens(user);
        _context.Update(user);
        _context.SaveChanges();
        if (tokenBdd.Token != null) return new AuthenticateResponse(user, jwtToken, tokenBdd.Token);
        throw new AppException("Token is null");
    }

    public AuthenticateResponse RefreshToken(string token, string ipAddress)
    {
        var user = GetUserByRefreshToken(token);
        if (user.RefreshTokens != null)
        {
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) {
                RemoveOldRefreshTokens(user);
                _context.Update(user);
                _context.SaveChanges();
            }
            if(refreshToken.Token != null && refreshToken.TokenExpires > DateTime.UtcNow) {
                    throw new AppException("Token est encore valide");
            }
          else if(refreshToken.newToken != null && refreshToken.newTokenExpires > DateTime.UtcNow) {
               throw new AppException("RefreshToken est encore valide");
          } else if(refreshToken.newTokenExpires < DateTime.UtcNow ) {
                throw new AppException("RefreshToken est expiré");
            } 
            refreshToken.newToken = _jwtUtils.GenerateRefreshToken(ipAddress).newToken;
            refreshToken.newTokenExpires = DateTime.UtcNow.AddDays(7);
                        user.RefreshTokens.Add(refreshToken);
            _context.Update(refreshToken);
            _context.SaveChanges();
            var jwtToken = _jwtUtils.GenerateToken(user);
            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }
        throw new Exception("refresh token is null");
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

    public void RevokeToken(string token, string ipAddress)
    {
        var user = GetUserByRefreshToken(token);
        {
            if (user.RefreshTokens != null)
            {
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
        
            }
        }

        _context.Update(user);
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

    private User GetUserByRefreshToken(string token)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshTokens != null && u.RefreshTokens.Any(t => t.Token == token));

        if (user == null)
            throw new AppException("Invalid token");

        return user;
    }

    private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        if(newRefreshToken is null)
        {
            throw new Exception("token is null");
        }
        newRefreshToken.Token = refreshToken.Token;
        return newRefreshToken;
    }


    private void RemoveOldRefreshTokens(User user)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        // if userId on get more than 2 refresh token, remove all the data raw for the userId
        if (user.RefreshTokens != null)
        {
           if(user.RefreshTokens.Count > 2)
           {
               user.RefreshTokens.RemoveAll( x => !user.RefreshTokens.OrderByDescending(x => x.Created).Take(2).Contains(x));
           }
           else
           {
               user.RefreshTokens.RemoveAll(x =>
                   !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTTl) <= DateTime.UtcNow);
           }
        }
    
    }

    // private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
    // {
    //     // recursively traverse the refresh token chain and ensure all descendants are revoked
    //     if (!string.IsNullOrEmpty(refreshToken.Token))
    //     {
    //         if (user.RefreshTokens != null)
    //         {
    //             var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.Token);
    //             if (childToken != null && childToken.IsActive)
    //                 RevokeRefreshToken(childToken, ipAddress, reason);
    //             else if (childToken != null) RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
    //         }
    //     }
    // }

    // private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason,
    //     string replacedByToken = null)
    // {
    //     token.Revoked = DateTime.UtcNow;
    //     token.RevokedByIp = ipAddress;
    //     token.ReasonRevoked = reason;
    //     token.ReplacedByToken = replacedByToken;
    // }

    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}