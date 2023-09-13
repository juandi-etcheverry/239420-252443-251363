using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
