using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Signup;

namespace WebApi.Controllers.Users;

[Route("api/signup")]
[ApiController]
public class SignupController : ControllerBase
{
    private readonly ISessionTokenLogic _sessionTokenLogic;
    private readonly IUserLogic _userLogic;

    public SignupController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }


    [HttpPost]
    [ServiceFilter(typeof(SignupAuthenticationFilter))]
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        var newUser = _userLogic.CreateUser(request.ToEntity());

        SessionToken tokenResponse;
        if (Request.Headers.ContainsKey("Authorization"))
        {
            var auth = Guid.Parse(Request.Headers["Authorization"]);
            if (_sessionTokenLogic.SessionTokenExists(auth))
                tokenResponse = _sessionTokenLogic.AddUserToToken(auth, newUser);
            else
                tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken { User = newUser });
        }
        else
        {
            tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken { User = newUser });
        }

        Response.Headers.Append("Authorization", tokenResponse.Id.ToString());

        var response = new SignupResponse { Message = "User created successfully" };
        return StatusCode(201, response);
    }
}