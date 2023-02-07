using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Models;
 
public class User
{
    // store in database 

    #region Properties

    public int Id { get; set; }
    public string? Username { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }
    public DateTime? TokenCreatedAt { get; set; }
    public DateTime? TokenExpires { get; set; }
    public string? RefreshToken { get; set; }


    //user data to object for form submission and validation

    #endregion
}
[Keyless]
public class UserDtoLogin
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

[Keyless]
public class UserDtoRegister
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}

// return only the data needed for the user
[Keyless]
public class UserReturnDto
{
    public string? FirstName { get; set; }
    public string? Username { get; set; }
    public string? LastName { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }
}