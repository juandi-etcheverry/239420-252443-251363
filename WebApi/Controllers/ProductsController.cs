using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        [Route("api/products")]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            return StatusCode(201, request);
        }


        [HttpGet]
        [Route("api/products")]
        public IActionResult GetProducts([FromQuery] GetProductsRequest request)
        {
            return StatusCode(200, "Products retrieved successfully");
        }

    }
}
