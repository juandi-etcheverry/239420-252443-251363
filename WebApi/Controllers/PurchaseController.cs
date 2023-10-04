using System.Security.Authentication;
using ApiModels.Requests;
using ApiModels.Responses;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Filters;
using WebApi.Filters.Logout;
using WebApi.Filters.User.Admin;

namespace WebApi.Controllers;

[ApiController]
[ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
public class PurchaseController : ControllerBase
{
    private IPurchaseLogic _purchaseLogic;
    private readonly ISessionTokenLogic _sessionTokenLogic;
    private readonly IUserLogic _userLogic;

    public PurchaseController(IPurchaseLogic purchaseLogic, ISessionTokenLogic sessionTokenLogic, IUserLogic userLogic)
    {
        _purchaseLogic = purchaseLogic;
        _sessionTokenLogic = sessionTokenLogic;
        _userLogic = userLogic;
    }

    [HttpGet]
    [Route("api/purchase/{id:guid}")]
    [ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
    public IActionResult GetPurchaseHistory([FromRoute] Guid id)
    {
        User user = _userLogic.GetUser(id);
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        var userAuth = _sessionTokenLogic.GetSessionToken(auth).User;
        if (userAuth.Role == Role.Admin || userAuth.Id != user.Id)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var response = new GetPurchasesHistoryResponse()
            { Purchases = _purchaseLogic.GetAllPurchasesHistory(user) };
        return StatusCode(200, response);
    }
    
    [HttpPost]
    [Route("api/purchase")]
    [ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
    public IActionResult AddPurchase([FromBody] AddPurchaseRequest request)
    {
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        var user = _sessionTokenLogic.GetSessionToken(auth).User;
        if (user.Role == Role.Admin)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var purchase = _purchaseLogic.AddCart(request.ToEntity());
        if (purchase.User.Id != user.Id)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var response = new EffectPurchaseResponse() { Purchase = purchase };
        return StatusCode(200, response);
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