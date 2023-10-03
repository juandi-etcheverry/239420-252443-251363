using ApiModels.Responses.Users;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Logout;

namespace WebApi.Controllers.Users
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public LogoutController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _userLogic = userLogic;
            _sessionTokenLogic = sessionTokenLogic;
        }

        [HttpPost]
        [ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
        public IActionResult Logout()
        {
            return StatusCode(200, new LogoutResponse() {Messsage = "Logout successful"});
        }
    }
}
