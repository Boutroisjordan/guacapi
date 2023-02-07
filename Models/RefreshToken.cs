namespace GuacAPI.Models;

 
public class RefreshToken
{
    // if we want to store on db the token with id for the specific user
    public int Id { get; set; }
    public string? Token { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Expires { get; set; }
}