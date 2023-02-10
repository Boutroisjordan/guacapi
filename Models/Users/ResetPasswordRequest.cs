using System.ComponentModel.DataAnnotations;


public class ResetPasswordRequest
{
    [Required] public string? Email { get; set; }
    [Required] public string? Token { get; set; }
    [Required, MinLength(6,ErrorMessage = "Please enter at least 6 characters minimum")] public string? Password { get; set; }
    [Required, Compare("Password")] public string? ConfirmPassword { get; set; }
}