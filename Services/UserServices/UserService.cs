using System.Net;
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

    private readonly DataContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UserService(DataContext context,
        IJwtUtils jwtUtils, IMapper mapper, IOptions<AppSettings> appSettings)
    {

        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    // pas mettre de variable dans la function, mais utiliser le _httpContextAccessor



    public Task<User> GetUserById(int id)
    {
        GetUser(id);
        var userId = _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        return userId;
    }


    //todo verifier si email deja existant
 public IEnumerable<User> GetAllUsersWithRoleId(int id)
    {
        var users = _context.Users.Where(u => u.RoleId == id).ToList();
        return users;
    }
    public async Task<User> GetUserByUsername(string username)
    {

        var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    
    public async Task<User> UpdateUser(int id, UpdateRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        var newProduct = _mapper.Map(request, user);
        if (user == null) return null;
        user.Username = request.Username;
        user.Email = request.Email;
    
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null) return null;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public RegisterResponse Register(RegisterRequest request)
    {
        if (_context.Users.Any(x => x.Username == request.Username) ||
            _context.Users.Any(x => x.Email == request.Email))
            throw new AppException("Username '" + request.Username + "' is already taken" +
                                   " or email '" + request.Email + "' is already taken");

        var register = _mapper.Map<User>(request);
        register.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        register.RoleId = 2;
        register.VerifyToken = Guid.NewGuid().ToString();
        
        _context.Users.Add(register);
        _context.SaveChanges();

        return new RegisterResponse(register, register.VerifyToken);
    }

    public AuthenticateResponse Login(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);
        // var user = await _context.Users.Where(u => u.Username == model.Username).SingleOrDefaultAsync();

        if (user == null || BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash) == false)
            throw new AppException("Username or password is incorrect");
       if(user.VerifiedAt == default(DateTime))
            throw new WebException("Please verify your email address");
        var AccessToken = _jwtUtils.GenerateAccessToken(user);

        // Save changes to the database
        _context.SaveChanges();

        return new AuthenticateResponse(user, AccessToken.AccessToken, AccessToken.NewToken, AccessToken.AccessTokenExpires, AccessToken.NewTokenExpires);
    }

public void VerifyEmail(string token, string email) {
    var user = _context.Users.SingleOrDefault(u => u.VerifyToken == token && u.Email == email);
    if (user == null) {
        throw new AppException("Token is incorrect");
    }
    user.VerifiedAt = DateTime.UtcNow;
    user.VerifyToken = null;
    _context.Users.Update(user);
    _context.SaveChanges();

}

    



    public void ResetPassword(string email)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        // validate
        if (user == null) throw new AppException("Email '" + email + "' is not found");
        user.VerifyToken = Guid.NewGuid().ToString();
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void ChangePassword(ChangePasswordRequest model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
        // validate
        if (user == null) throw new AppException("Email '" + model.Email + "' is not found");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword);
        user.VerifyToken = null;
        _context.Users.Update(user);
        _context.SaveChanges();
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
        Console.WriteLine("token: " + token);
        var user = _context.Users.Include(u => u.RefreshToken)
                              .FirstOrDefault(u => u.RefreshToken.AccessToken == token || u.RefreshToken.NewToken == token);
        if (user == null) throw new KeyNotFoundException("Tous les tokens sont expirés");
        return user;
    }




    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}