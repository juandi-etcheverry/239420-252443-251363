using Domain;

namespace ApiModels.Responses.Users;

public class CreateUserResponse
{
    public string Message { get; set; }
    public User? User { get; set; }
}