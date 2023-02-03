namespace GuacAPI.Helpers;

public class AppSettings
{
    public string Secret { get; set; }
    
    public int RefreshTokenTTl { get; set; }
}