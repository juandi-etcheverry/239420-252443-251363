using ApiModels.Requests.Users;
using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.User;

namespace WebApi.Controllers.Users
{
    [Route("/api/users/{id}")]
    [ApiController]
    [ServiceFilter(typeof(UserAuthenticationFilter))]
    public class UsersController : ControllerBase
    {

        private IUserLogic _userLogic;
        private ISessionTokenLogic _sessionTokenLogic;
        public UsersController(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _userLogic = userLogic;
            _sessionTokenLogic = sessionTokenLogic;
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
            User user = _userLogic.UpdateUser(id, request.ToEntity());
            var response = new UpdateUserResponse()
            {
                Email = user.Email,
                Address = user.Address,
                Role = user.Role,
            };

            return StatusCode(200, response);
        }
    }
}
