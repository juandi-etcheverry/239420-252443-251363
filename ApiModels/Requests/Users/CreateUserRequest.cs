using Domain;
using TypeHelper;

namespace ApiModels.Requests.Users;

public class CreateUserRequest
{
    public string Email { get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }

    public User ToEntity()
    {
        return new User
        {
            Email = Email,
            Address = Address,
            Role = Role,
            Password = Password
        };
    }
}