using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : ControllerBase
    {
        [Route("api/products")]
        public IActionResult CreateProduct(CreateProductRequest request)
        {
            return StatusCode(201, "Product created successfully");
        }
    }
}
