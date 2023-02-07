using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

public class RegisterRequest
{
    [Required] public string? Username { get; set; }
    [Required, MinLength(6,ErrorMessage = "Please enter at least 6 characters minimum")] public string? Password { get; set; }
    [Required, Compare("Password")] public string? ConfirmPassword { get; set; }
    [Required, EmailAddress] public string? Email { get; set; }
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Required] public string? Phone { get; set; }
}