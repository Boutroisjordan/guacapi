using System.Text.Json.Serialization;
using GuacAPI.Entities;

namespace GuacAPI.Models.Users;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }
    public DateTime TokenExpires { get; set; }
    public DateTime RefreshExpires { get; set; }

    [JsonIgnore] public string RefreshToken { get; set; }

    public AuthenticateResponse(User user, string jwtToken, string refreshToken, DateTime tokenExpires, DateTime refreshExpires)
    {
        Id = user.UserId;
        Username = user.Username;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        TokenExpires = tokenExpires;
        RefreshExpires = refreshExpires;
    
    }
}