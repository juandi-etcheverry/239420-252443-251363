using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using DataAccess;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Signup([FromBody] SignupRequest request, [FromHeader(Name = "Authorization")] Guid auth)
        {
            if (_sessionTokenLogic.GetSessionToken(auth).User != null)
            {
                 var responseError = new SignupResponse() { Message = "You are already logged in !" };
                 return StatusCode(400, responseError);
            }

            User newUser = _userLogic.CreateUser(request.ToEntity());
            var response = new SignupResponse() { Message = "User created successfully" };
            return StatusCode(201, response);
        }
    }
}

