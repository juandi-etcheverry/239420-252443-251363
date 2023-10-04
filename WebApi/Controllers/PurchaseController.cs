using System.Security.Authentication;
using ApiModels.Requests;
using ApiModels.Responses;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Filters.Logout;
using WebApi.Filters.Purchase;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers;

[ApiController]
public class PurchaseController : ControllerBase
{
    private IPurchaseLogic _purchaseLogic;
    private readonly ISessionTokenLogic _sessionTokenLogic;
    private readonly IUserLogic _userLogic;
    private readonly IProductLogic _productLogic;

    public PurchaseController(IPurchaseLogic purchaseLogic, ISessionTokenLogic sessionTokenLogic, IUserLogic userLogic, IProductLogic productLogic)
    {
        _purchaseLogic = purchaseLogic;
        _sessionTokenLogic = sessionTokenLogic;
        _userLogic = userLogic;
        _productLogic = productLogic;
    }

    [HttpGet]
    [Route("api/purchase/{id:guid}")]
    [ServiceFilter(typeof(GetPurchaseHistoryFilter))]
    public IActionResult GetPurchaseHistory([FromRoute] Guid id)
    {
        User user = _userLogic.GetUser(id);
        List<Purchase> purchases = _purchaseLogic.GetAllPurchasesHistory(user);
        var response = new GetPurchasesHistoryResponse()
        {
            Message = "Purchases retrieved successfully",
            Purchases = purchases.Select(p => GetPurchasesHistoryResponse.ToPurchaseDTO(p)).ToList()
        };
        return StatusCode(200, response);
    }
    
    [HttpPost]
    [Route("api/purchase")]
    [ServiceFilter(typeof(AddPurchaseFilter))]
    public IActionResult AddPurchase([FromBody] AddPurchaseRequest request)
    {
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        var user = _sessionTokenLogic.GetSessionToken(auth).User;

        List<Product> products = _productLogic.GetProducts(p => request.ProductsIds.Contains(p.Id));

        var newPurchase = new Purchase()
        {
            User = user
        };
        newPurchase.AddProducts(products);
        var purchase = _purchaseLogic.AddCart(newPurchase);
 
        var response = new EffectPurchaseResponse() { Purchase = purchase }; //traer solo lo relevante, no todo el purchase que incluye user.
        return StatusCode(201, response);
    }

    [HttpGet]
    [Route("api/purchase")]
    [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
    public IActionResult GetUserPurchaseHistory()
    {
        var allPurchases = _purchaseLogic.GetAllPurchasesHistory();
        var response = new ManyPurchasesResponse()
        {
            Purchases = allPurchases,
        };
        return StatusCode(200, response);
    }

}