using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using GuacAPI.Authorization;
using GuacAPI.Context;
using GuacAPI.Helpers;
using GuacAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GuacAPI.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public UserService(IHttpContextAccessor httpContextAccessor, DataContext context, IConfiguration configuration,
        IJwtUtils jwtUtils, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _configuration = configuration;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

    // pas mettre de variable dans la function, mais utiliser le _httpContextAccessor

    public object? GetUserInfos()
    {
        var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        var role = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ??
                   throw new ArgumentNullException(
                       "_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value");

        //   var user = _context.Users.FirstOrDefault(u => u.Username == username);

        return new { username, role };
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public Task<User?> GetUserById(int id)
    {
        getUser(id);
        var userId = _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return userId;
    }

    public async Task<User?> updateToken(User request)
    {
        getUser(request.Id);

        return await UpdateUser(request, request.Id);
    }


    //todo verifier si email deja existant

    public async Task<User?> GetUserByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }


    public async Task<User?> UpdateUser(User request, int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        user.Username = request.Username;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Register(User user)
    {
        if (await _context.Users.AnyAsync(x => x.Username == user.Username))
            throw new AppException("Username '" + user.Username + "' is already taken");

        var register = _mapper.Map<User>(user);
        register.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        var savedRegister = _context.Users.Add(register).Entity;
        await _context.SaveChangesAsync();
        return savedRegister;
    }

    public async Task<User?> Login(UserDtoLogin request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (request.Password is null)
        {
            throw new Exception("Password is null");
        }

        var response = _mapper.Map<User>(user);
        if (user != null)
        {
            response.Token = _jwtUtils.GenerateToken(user);
            response.TokenExpires = DateTime.Now.AddMinutes(1);
            var result = VerifyPasswordHash(request);
            if (result == false)
            {
                throw new Exception("Password is incorrect");
            }

            return user;
        }

        throw new Exception("User doesn't exist");
    }

    public bool VerifyPasswordHash(UserDtoLogin request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if (user == null) return false;
        var result = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        return result;
    }

    public async Task<bool> CheckUsernameAvailability(string username)
    {
        var checkUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (checkUsername != null)
        {
            return false;
        }

        return true;
    }

    public string CreateToken(User user)
    {
        if (user.Username != null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Secret").Value ?? throw new InvalidOperationException()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        return String.Empty;
    }

    private User getUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        if (user.TokenExpires < DateTime.Now)
        {
            user.RefreshToken = _jwtUtils.RefreshToken(user);
            user.TokenExpires = DateTime.Now.AddDays(7);
            _context.SaveChanges();
        }

        ;
        return user;
    }
}