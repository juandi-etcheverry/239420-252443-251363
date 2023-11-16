using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Products
{
    [ApiController]
    [Route("api/brands")]
    public class GetBrandsController : ControllerBase
    {
        private readonly IBrandLogic _brandLogic;

        public GetBrandsController(IBrandLogic brandLogic)
        {
            _brandLogic = brandLogic;
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            return StatusCode(200, _brandLogic.GetAllBrands().ToList());
        }
    }
}
