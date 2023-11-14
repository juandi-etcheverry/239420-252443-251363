using ApiModels.Requests;
using ApiModels.Responses.Purchases;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters.Purchase;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers.Purchases;

[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IProductLogic _productLogic;
    private readonly ISessionTokenLogic _sessionTokenLogic;
    private readonly IUserLogic _userLogic;
    private readonly IPurchaseLogic _purchaseLogic;

    public PurchaseController(IPurchaseLogic purchaseLogic, ISessionTokenLogic sessionTokenLogic, IUserLogic userLogic,
        IProductLogic productLogic)
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
        var user = _userLogic.GetUser(id);
        var purchases = _purchaseLogic.GetAllPurchasesHistory(user);
        var response = new GetPurchasesHistoryResponse
        {
            Message = "Purchases retrieved successfully",
            Purchases = purchases.Select(p => PurchaseDTO.ToPurchaseDTO(p)).ToList()
        };
        return StatusCode(200, response);
    }

    [HttpPost]
    [Route("api/purchases")]
    [ServiceFilter(typeof(AddPurchaseFilter))]
    public IActionResult AddPurchase([FromBody] AddPurchaseRequest request, [FromHeader] Guid Authorization)
    {
        var user = _sessionTokenLogic.GetSessionToken(Authorization).User;

        _productLogic.IsPurchaseValid(request.Cart);
        foreach (var pp in request.Cart)
        {
            
            _productLogic.DecreaseStock(pp.ProductId, pp.Quantity);
        }

        var newPurchase = new Purchase { User = user };

        var products = _productLogic.GetProducts(p =>
        {
            var productsIds = request.Cart.Select(p => p.ProductId).ToList();
            return productsIds.Contains(p.Id);
        });

        newPurchase.AddProducts(products);
        _purchaseLogic.ValidatePaymentMethod(request.PaymentMethod);
        newPurchase.PaymentMethod = request.PaymentMethod;
        var purchase = _purchaseLogic.AddCart(newPurchase);
        purchase.PaymentMethod = request.PaymentMethod;

        var response = new EffectPurchaseResponse
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
        var response = new ManyPurchasesResponse
        {
            Purchases = allPurchases.Select(p => PurchaseDTO.ToPurchaseDTO(p)).ToList()
        };
        return StatusCode(200, response);
    }
}