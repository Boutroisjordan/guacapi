using System.Text.Json.Serialization;
using GuacAPI.Entities;

namespace GuacAPI.Models.Users;

public class AuthenticateResponse
{
    // public int Id { get; set; }
    public string? Username { get; set; }
    public string? JwtToken { get; set; }
    
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    

}