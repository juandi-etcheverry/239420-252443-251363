using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers.Users;

[Route("api/users")]
[ApiController]
public class CreateUserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private ISessionTokenLogic _sessionTokenLogic;

    public CreateUserController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var newUser = _userLogic.CreateUser(request.ToEntity());
        var responseOk = new CreateUserResponse { Message = "User created successfully", User = newUser };
        return StatusCode(201, responseOk);
    }
}