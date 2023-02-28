namespace GuacAPI.Models.Users;

public class UpdateRequestAdmin
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public int RoleId { get; set; }
}

public class UserUpdateRequest {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

}