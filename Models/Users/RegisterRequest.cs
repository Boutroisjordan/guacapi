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

         public string Ville { get; set; }

         public string Pays { get; set; }

     public string Adress { get; set; }

         public string Rue { get; set; }

         public string CodePostal { get; set; }

         public string Bio { get; set; }


}