using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

public class RegisterRequest
{
    [Required] public string Username { get; set; }
    [Required, 
    StringLength(50,ErrorMessage = "Please enter at least 6 characters minimum",MinimumLength = 6)]
     public string Password { get; set; }
    [Required,DataType(DataType.Password), Compare("Password")] public string ConfirmPassword { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Phone { get; set; }

    [Required] public string Ville { get; set; }

    [Required] public string Pays { get; set; }

    [Required] public string Adress { get; set; }

    [Required] public string Rue { get; set; }

    [Required] public string CodePostal { get; set; }

    [Required] public string Bio { get; set; }


}