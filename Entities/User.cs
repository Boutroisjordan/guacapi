using System.Text.Json.Serialization;
using GuacAPI.Models;
namespace GuacAPI.Entities;

public class User
{
    // store in database 

    #region Properties

    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Role { get; set; }

    public DateTime VerifiedAt { get; set; }

    [JsonIgnore] public string PasswordHash { get; set; }
    [JsonIgnore] public List<RefreshToken> RefreshTokens { get; set; }

    public List<Comment> Comments {get; set;}


    //user data to object for form submission and validation

    #endregion
}