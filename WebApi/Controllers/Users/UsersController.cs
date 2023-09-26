using ApiModels.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("users/{id}")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetUser([FromRoute] string id, [FromQuery] GetUserRequest request)
        {
            return StatusCode(200, "Users retrieved successfully");
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromRoute] string id)
        {
            return StatusCode(200, "User deleted successfully");
        }

        [HttpPut]
        public IActionResult UpdateUser([FromRoute] string id, [FromBody] UpdateUserRequest request)
        {
            return StatusCode(200, "User updated successfully");
        }
    }
}
