using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [ApiController]
    [Route("api/colors")]
    public class GetColorsController : ControllerBase
    {

        private readonly IColorLogic _colorLogic;

        public GetColorsController(IColorLogic colorLogic)
        {
            _colorLogic = colorLogic;
        }

        [HttpGet]
        public IActionResult GetAllColors()
        {
            return StatusCode(200, _colorLogic.GetAllColors().ToList());
        }
    }
}
