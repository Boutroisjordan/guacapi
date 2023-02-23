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
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpires { get; set; }

    public string NewToken { get; set; }
    public DateTime NewTokenExpires { get; set; }
    public DateTime Created { get; set; }

    public bool IsTokenExpired => DateTime.UtcNow >= AccessTokenExpires;
    public bool IsNewTokenExpired => DateTime.UtcNow >= NewTokenExpires;
    public bool IsActive => !IsNewTokenExpired;
    public User User { get; set; }
    public int UserId { get; set; }


} // navigation property