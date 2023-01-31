using GuacAPI.Models;

namespace GuacAPI.Services.UserServices;

public interface IUserService
{
    object? GetUserInfos();

    Task<List<User>> GetAllUsers();

    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserByEmail(string email);

    Task<User?> UpdateUser(User user, int id);
    Task<User?> DeleteUser(int id);
    Task<User?> Register(User user);
}