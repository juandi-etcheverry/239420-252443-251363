using ApiModels.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        [HttpPost]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            return StatusCode(201, "User created successfully");
        }
    }
}
