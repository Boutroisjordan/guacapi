using System.Security.Claims;
using GuacAPI.Context;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace GuacAPI.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public UserService(IHttpContextAccessor httpContextAccessor, DataContext context, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _configuration = configuration;
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
    public async Task<User?> updateToken(UserDtoLogin request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if(user is null) {
            throw new Exception("User doesn't exist");
        }

        string token = CreateToken(user);

        user.Token = token;
        // user.RefreshToken = request.RefreshToken;
        // user.TokenExpires = request.TokenExpires;
        // user.TokenCreatedAt = request.CreatedAt;
        
        await _context.SaveChangesAsync();
        return user;
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

     public async Task<bool> Login(string username, string password)
     {

         var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);


         if(user is null) {
            return false;
         }
         if(user.PasswordHash is null || user.PasswordSalt is null) {
            return false;
         }
        
        var result = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        
         return result;
     }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
        public async Task<bool> CheckUsernameAvailability(string username)
        {
            var checkUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if(checkUsername != null) {
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
//todo création d'admin de base

}