using Domain.Validation;
using TypeHelper;

namespace Domain;

public class User
{
    private string _address;
    private string _email;
    private string _password;
    public Guid Id { get; set; }

    public string Email
    {
        get => _email;
        set
        {
            ValidateUser.ValidateEmail(value);
            _email = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            ValidateUser.ValidatePassword(value);
            _password = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            ValidateUser.ValidateAddress(value);
            _address = value;
        }
    }

    public Role Role { get; set; }

    public bool IsDeleted { get; set; } = false;
}