using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using DataAccess;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Filters;
using WebApi.Filters.Signup;


namespace WebApi.Controllers.Users
{

    [Route("api/signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private IUserLogic _userLogic;
        private ISessionTokenLogic _sessionTokenLogic;
        public SignupController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _userLogic = userLogic;
            _sessionTokenLogic = sessionTokenLogic;
        }

        
        [HttpPost]
        [ServiceFilter(typeof(SignupAuthenticationFilter))]
        public IActionResult Signup([FromBody] SignupRequest request, [FromHeader(Name = "Cookie")] string? header)
        {
            User newUser = _userLogic.CreateUser(request.ToEntity());

            SessionToken tokenResponse;
            if (CookieValidation.AuthExists(header))
            {
                Guid auth = CookieValidation.GetAuthFromHeader(header);
                if (_sessionTokenLogic.SessionTokenExists(auth))
                {
                    tokenResponse = _sessionTokenLogic.AddUserToToken(auth, newUser);
                }
                else
                {
                    tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken() { User = newUser });
                }
            }
            else
            {
                tokenResponse = _sessionTokenLogic.AddSessionToken(new SessionToken() { User = newUser });
            }
 
            Response.Cookies.Append("Authorization", tokenResponse.Id.ToString(), new CookieOptions(){ HttpOnly = true});

            var response = new SignupResponse() { Message = "User created successfully" };
            return StatusCode(201, response);
        }
    }
}

