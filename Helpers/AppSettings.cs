namespace GuacAPI.Helpers;

public class AppSettings
{
    public string Secret { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}