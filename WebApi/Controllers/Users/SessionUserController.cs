using System;
using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Logout;
using WebApi.Filters.Sessions;

namespace WebApi.Controllers.Users;

[Route("api/sessions/user")]
[ApiController]
public class SessionUserController : ControllerBase
{
	private readonly ISessionTokenLogic _sessionTokenLogic;

    public SessionUserController(ISessionTokenLogic sessionTokenLogic)
	{
		_sessionTokenLogic = sessionTokenLogic;
    }

	[HttpGet]
    [ServiceFilter(typeof(IsLoggedFilter))]
	public IActionResult GetLoggedUser([FromHeader] Guid Authorization) 
	{
        var user = _sessionTokenLogic.GetSessionToken(Authorization).User;
        var response = new GetUserResponse
        {
            Id = user.Id,
            Message = "User found",
            Address = user.Address,
            Email = user.Email,
            Role = user.Role
        };
        return StatusCode(200, response);
    }

}


