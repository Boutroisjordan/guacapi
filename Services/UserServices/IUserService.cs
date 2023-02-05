using GuacAPI.Entities;
using GuacAPI.Models;
using GuacAPI.Models.Users;

namespace GuacAPI.Services.UserServices;
public interface IUserService
{

    Task<List<User>> GetAllUsers();

    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUsername(string username);
    Task<User?> GetUserByEmail(string email);

    Task<User?> UpdateUser(User user, int id);
    Task<User?> DeleteUser(int id);
    void Register(RegisterRequest request);
    AuthenticateResponse Login(AuthenticateRequest request, string ipAddress);
    AuthenticateResponse RefreshToken(string token, string ipAddress);
    void Update(int id, UpdateRequest model);
    void RevokeToken(string token, string ipAddress);
    IEnumerable<User> GetAll();
    User GetById(int id);
}