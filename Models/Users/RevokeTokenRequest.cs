namespace GuacAPI.Models;

public class RevokeTokenRequest
{
    public string? Token { get; set; }
    public string? Reason { get; set; }
}