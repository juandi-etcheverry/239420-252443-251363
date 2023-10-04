using System.Security.Authentication;
using ApiModels.Requests;
using ApiModels.Responses.Purchases;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Filters.Logout;
using WebApi.Filters.Purchase;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers.Purchases;

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
    [Route("api/purchases/{id:guid}")]
    [ServiceFilter(typeof(GetPurchaseHistoryFilter))]
    public IActionResult GetMyPurchaseHistory([FromRoute] Guid id)
    {
        User user = _userLogic.GetUser(id);
        List<Purchase> purchases = _purchaseLogic.GetAllPurchasesHistory(user);
        var response = new GetPurchasesHistoryResponse()
        {
            Message = "Purchases retrieved successfully",
            Purchases = purchases.Select(p => PurchaseDTO.ToPurchaseDTO(p)).ToList()
        };
        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("api/purchases")]
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

        var response = new EffectPurchaseResponse()
        {
            Message = "Purchase created successfully",
            Purchase = PurchaseDTO.ToPurchaseDTO(purchase)
        };
        return StatusCode(201, response);
    }

    [HttpGet]
    [Route("api/purchases")]
    [ServiceFilter(typeof(AdminUserAuthenticationFilter))]
    public IActionResult GetAllPurchasesHistory()
    {
        var allPurchases = _purchaseLogic.GetAllPurchasesHistory();
        var response = new ManyPurchasesResponse()
        {
            Purchases = allPurchases.Select(p => PurchaseDTO.ToPurchaseDTO(p)).ToList()
        };
        return StatusCode(200, response);
    }

}