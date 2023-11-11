using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [ApiController]
    [Route("api/categories")]
    public class GetCategoriesController : ControllerBase
    {

        private readonly ICategoryLogic _categoryLogic;

        public GetCategoriesController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return StatusCode(200, _categoryLogic.GetAllCategories().ToList());
        }
    }
}
