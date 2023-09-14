using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            return StatusCode(201, "Product created successfully");
        }


        [HttpGet]
        public IActionResult GetProducts([FromQuery] GetProductsRequest request)
        {
            return StatusCode(200, "Products retrieved successfully");
        }

    }
}
