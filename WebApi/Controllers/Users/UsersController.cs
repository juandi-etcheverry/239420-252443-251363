using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [Route("users/{id}")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserLogic _userLogic;
        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            User user = _userLogic.GetUser(id);
            var response = new GetUserResponse()
            {
                Address = user.Address,
                Email = user.Email,
                Role = user.Role,
            };
            return StatusCode(200, response);
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            User user = _userLogic.DeleteUser(id);
            var response = new DeleteUserResponse()
            {
               Email = user.Email,
               Address = user.Address,
               Role = user.Role,
            };

            return StatusCode(200, response);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            return StatusCode(200, "User updated successfully");
        }
    }
}
