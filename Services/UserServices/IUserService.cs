﻿using GuacAPI.Entities;
using GuacAPI.Models;
using GuacAPI.Models.Users;

namespace GuacAPI.Services.UserServices;
public interface IUserService
{

    Task<List<User>> GetAllUsers();

    Task<User> GetUserById(int id);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserByEmail(string email);
    Task<User> UpdateUser(int id, UpdateRequest model);
    Task<User> DeleteUser(int id);
    RegisterResponse Register(RegisterRequest request);
    // AuthenticateResponse Login(AuthenticateRequest request);
    AuthenticateResponse Login(AuthenticateRequest model);
    User GetUserByRefreshToken(string token);
    void Update(int id, UpdateRequest model);
    void ResetPassword(string email);
     void  VerifyEmail(string token,string email);
    IEnumerable<User> GetAll();
    User GetById(int id);
}