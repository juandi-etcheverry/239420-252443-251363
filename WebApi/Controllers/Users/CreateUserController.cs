using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.User;

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
    [ServiceFilter(typeof(UserAuthenticationFilter))]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var newUser = _userLogic.CreateUser(request.ToEntity());
        var responseOk = new CreateUserResponse { Message = "User created successfully", User = newUser };
        return StatusCode(201, responseOk);
    }
}