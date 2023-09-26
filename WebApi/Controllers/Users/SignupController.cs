using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;


namespace WebApi.Controllers.Users
{

    [Route("api/signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private IUserLogic _userLogic;
        public SignupController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            User newUser = _userLogic.CreateUser(request.ToEntity());
            var response = new SignupResponse() { Message = "User created successfully" };
            return StatusCode(201, response);
        }
    }
}

