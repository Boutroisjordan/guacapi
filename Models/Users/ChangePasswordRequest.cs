using System.ComponentModel.DataAnnotations;

public class ChangePasswordRequest
{
    public string Email { get; set; }
      [Required,DataType(DataType.Password), Compare("Password")] public string ConfirmPassword { get; set; }

}