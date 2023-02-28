using GuacAPI.Entities;

public class RegisterResponse {
    public string VerifyToken { get; set; }
    public string Email { get; set; }

    public RegisterResponse(User user, string verifyToken) {
        VerifyToken = verifyToken;
        Email = user.Email;
    }
}