using ApiModels.Responses;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/cart")]
[ApiController]
[ServiceFilter(typeof(CartAuthenticationFilter))]
public class CartController : ControllerBase
{
    private IPurchaseLogic _purchaseLogic;
    private readonly ISessionTokenLogic _sessionTokenLogic;
    
    public CartController(IPurchaseLogic purchaseLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _purchaseLogic = purchaseLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }
    
    [HttpGet]
    public IActionResult GetCart()
    {
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        Purchase purchase = _sessionTokenLogic.GetSessionToken(auth).Cart;
        var response = new GetCartResponse()
        {
            Products = purchase.Products,
            Message = "Cart retrieved successfully"
        };
        return StatusCode(200, response);
    }
    
    //[HttpPut]
     
    
}