using ApiModels.Requests;
using ApiModels.Responses.Products;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Products;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [ServiceFilter(typeof(ProductAuthenticationFilter))]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public ProductsController(IProductLogic productLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _productLogic = productLogic;
            _sessionTokenLogic = sessionTokenLogic;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = _productLogic.AddProduct(request.ToEntity());
            var response = new CreateProductResponse()
            {
                Message = "Product created successfully",
                Name = request.Name,
                Brand = request.Brand,
                Category = request.Category,
                Colors = request.Colors,
                Description = request.Description,
                Price = request.Price
            };
            return StatusCode(201, response);
        }


        [HttpGet]
        public IActionResult GetProducts([FromQuery] GetProductsRequest request)
        {
            var products = _productLogic.GetProducts(p => p.Name.Contains(request.Text) ||
                                                          p.Description.Contains(request.Text) ||
                                                          p.Brand.Name.Contains(request.Brand) ||
                                                          p.Category.Name.Contains(request.Category));

             var response = new GetProductsResponse()
            {
                Message = "Products retrieved successfully",
                Products = products.Select(p => GetProductsResponse.ToResponseObject(p)).ToList()
            };

             return StatusCode(200, response);
        }

    }
}
