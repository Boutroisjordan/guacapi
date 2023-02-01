﻿using GuacAPI.Models;

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
    Task<User?> updateToken(UserDtoLogin request);
    Task<bool> Login(string username, string password);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    Task<bool> CheckUsernameAvailability(string username);

    string CreateToken(User user);
}