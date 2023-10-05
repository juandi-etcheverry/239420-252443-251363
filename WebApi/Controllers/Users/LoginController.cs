using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Login;

namespace WebApi.Controllers.Users;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    private readonly IUserLogic _userLogic;

    public LoginController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(LoginAuthenticationFilter))]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userLogic.GetUser(request.Email, request.Password);

        SessionToken tokenResponse;
        if (Request.Cookies.ContainsKey("Authorization"))
        {
            var auth = Guid.Parse(Request.Cookies["Authorization"]);
            if (_sessionTokenLogic.SessionTokenExists(auth))
                tokenResponse = _sessionTokenLogic.AddUserToToken(auth, user);
            else
                tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken { User = user });
        }
        else
        {
            tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken { User = user });
            Response.Cookies.Append("Authorization", tokenResponse.Id.ToString(),
                new CookieOptions { HttpOnly = true });
        }


        var response = new LoginResponse
        {
            Message = "User logged in successfully",
            Address = user.Address,
            Email = user.Email,
            Role = user.Role
        };

        return StatusCode(200, response);
    }
}