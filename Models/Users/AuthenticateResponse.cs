using System.Text.Json.Serialization;
using GuacAPI.Entities;

namespace GuacAPI.Models.Users;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    
    public string AccessToken { get; set; }
    public DateTime TokenExpires { get; set; }
    public DateTime RefreshExpires { get; set; }

    public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string accessToken, string refreshToken, DateTime tokenExpires, DateTime refreshExpires)
    {
        Id = user.UserId;
        Username = user.Username;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        RoleId = user.RoleId;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        TokenExpires = tokenExpires;
        RefreshExpires = refreshExpires;
    
    }

  
}