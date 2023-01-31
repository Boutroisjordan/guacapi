using System.Security.Claims;
using GuacAPI.Context;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;

    public UserService(IHttpContextAccessor httpContextAccessor, DataContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
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

    public async Task<User?> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

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
        var register = new User
        {
            Username = user.Username,
            Email = user.Email,
            Phone = user.Phone,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            Role = user.Role
        };
        var savedRegister = _context.Users.Add(register).Entity;
        await _context.SaveChangesAsync();
        return savedRegister;
    }
}