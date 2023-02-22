using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Entities;


[Owned]
public class RefreshToken
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpires { get; set; }

    public string newToken { get; set; }
    public DateTime newTokenExpires { get; set; }
    public DateTime Created { get; set; }

    public bool IsTokenExpired => DateTime.UtcNow >= TokenExpires;
    public bool IsNewTokenExpired => DateTime.UtcNow >= newTokenExpires;
    public bool IsActive => !IsNewTokenExpired;
    public User User { get; set; }
    public int UserId { get; set; }


} // navigation property