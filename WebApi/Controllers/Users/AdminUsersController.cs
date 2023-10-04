using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.User;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers.Users
{
    [Route("/api/users/{id}")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {

        private IUserLogic _userLogic;
        private ISessionTokenLogic _sessionTokenLogic;
        public AdminUsersController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _userLogic = userLogic;
            _sessionTokenLogic = sessionTokenLogic;
        }

        [HttpGet]
        [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            User user = _userLogic.GetUser(id);
            var response = new GetUserResponse()
            {
                Message = "User found",
                Address = user.Address,
                Email = user.Email,
                Role = user.Role,
            };
            return StatusCode(200, response);
        }

        [HttpDelete]
        [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            User user = _userLogic.DeleteUser(id);
            var response = new DeleteUserResponse()
            {
                Message = "User deleted",
               Email = user.Email,
               Address = user.Address,
               Role = user.Role,
            };

            return StatusCode(200, response);
        }

        [HttpPut]
        [ServiceFilter(typeof(UpdateUserAuthenticationFilter))]
        public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            User user = _userLogic.UpdateUser(id, request.ToEntity());
            var response = new UpdateUserResponse()
            {
                Message = "User updated",
                Email = user.Email,
                Address = user.Address,
                Role = user.Role,
            };

            return StatusCode(200, response);
        }
    }
}
