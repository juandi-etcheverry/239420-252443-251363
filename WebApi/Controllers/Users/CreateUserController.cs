using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.CreateUser;

namespace WebApi.Controllers.Users;

[Route("api/users")]
[ApiController]
public class CreateUserController : ControllerBase
{
    private ISessionTokenLogic _sessionTokenLogic;
    private readonly IUserLogic _userLogic;

    public CreateUserController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(CreateUserAuthenticationFilter))]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var newUser = _userLogic.CreateUser(request.ToEntity());
            var responseOk = new CreateUserResponse { Message = "User created successfully", User = newUser };
            return StatusCode(201, responseOk);
        }
        catch (ArgumentException e)
        {
            var responseAlreadyExists = new CreateUserResponse { Message = e.Message };
            return StatusCode(400, responseAlreadyExists);
        }
    }
}