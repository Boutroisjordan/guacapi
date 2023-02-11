using System.Text.Json.Serialization;

namespace GuacAPI.Entities;

public class User
{
    // store in database 

    #region Properties

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? VerifiedAt { get; set; }
    public Role? Role { get; set; }
    [JsonIgnore] public string PasswordHash { get; set; }
    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; }
                

    //user data to object for form submission and validation

    #endregion
}