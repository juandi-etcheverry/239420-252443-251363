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

    public CreateUserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
    public IActionResult CreateUser([FromBody] CreateUserRequest request)
    {
        var newUser = _userLogic.CreateUser(request.ToEntity());
        var responseOk = new CreateUserResponse { Message = "User created successfully", User = newUser };
        return StatusCode(201, responseOk);
    }

    [HttpGet]
    [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
    public IActionResult GetAllUsers()
    {
        var users = _userLogic.GetAllUsers().ToList();
        var response = new GetAllUsersResponse { Users = users };
        return StatusCode(200, response);
    }
}