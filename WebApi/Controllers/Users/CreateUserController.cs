using ApiModels.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            return StatusCode(201, "User created successfully");
        }
    }
}
