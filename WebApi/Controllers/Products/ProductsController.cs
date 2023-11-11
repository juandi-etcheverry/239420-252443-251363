using ApiModels.Requests;
using ApiModels.Responses.Products;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Products;

namespace WebApi.Controllers.Products;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductLogic _productLogic;

    public ProductsController(IProductLogic productLogic)
    {
        _productLogic = productLogic;
    }

    [HttpPost]
    [Route("api/products")]
    [ServiceFilter(typeof(ProductAuthenticationFilter))]
    public IActionResult CreateProduct([FromBody] CreateProductRequest request)
    {
        var product = _productLogic.AddProduct(request.ToEntity());
        var response = new CreateProductResponse
        {
            Message = "Product created successfully",
            Name = product.Name,
            Brand = product.Brand,
            Category = product.Category,
            Colors = product.Colors.ToList(),
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
        return StatusCode(201, response);
    }


    [HttpGet]
    [Route("api/products")]
    public IActionResult GetProducts([FromQuery] GetProductsRequest request)
    {
        var products = _productLogic.GetProducts(p =>((p.Name.ToLower().Contains(request.Text.ToLower()) ||
                                                       p.Description.ToLower().Contains(request.Text.ToLower()))) &&
                                                      p.Brand.Name.ToLower().Contains(request.Brand.ToLower()) &&
                                                      p.Category.Name.ToLower().Contains(request.Category.ToLower()) &&
                                                      p.Price >= request.MinPrice &&
                                                      p.Price <= request.MaxPrice &&
                                                      p.IsDeleted == false);

        var response = new GetProductsResponse
        {
            Message = "Products retrieved successfully",
            Products = products.Select(p => GetProductsResponse.ToResponseObject(p)).ToList(),
            Brands = GetProductsResponse.GetBrands(products),
            Categories = GetProductsResponse.GetCategories(products),
            Colors = GetProductsResponse.GetColors(products),
        };

        return StatusCode(200, response);
    }

    [HttpGet]
    [Route("api/products/{id}")]
    public IActionResult GetProduct([FromRoute] Guid id)
    {
        var product = _productLogic.GetProduct(id);

        var response = new GetProductResponse
        {
            id = product.Id,
            Message = "Product retrieved successfully",
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Brand = product.Brand,
            Category = product.Category,
            Colors = product.Colors,
            Stock = product.Stock
        };

        return StatusCode(200, response);
    }

    [HttpDelete]
    [Route("api/products/{id}")]
    [ServiceFilter(typeof(ProductAuthenticationFilter))]
    public IActionResult DeleteProduct([FromRoute] Guid id)
    {
        var product = _productLogic.SoftDelete(id);

        var response = new DeleteProductResponse
        {
            Message = "Product deleted successfully",
            ProductName = product.Name
        };

        return StatusCode(200, response);
    }

    [HttpPut]
    [Route("api/products")]
    [ServiceFilter(typeof(ProductAuthenticationFilter))]
    public IActionResult ModifyProduct([FromBody] ModifyProductRequest request)
    {
        var product = _productLogic.UpdateProduct(request.Id, request.ToEntity());
        var response = new ModifyProductResponse
        {
            Message = "Product updated successfully",
            Name = product.Name
        }; 
        return StatusCode(200, response);
    }

}