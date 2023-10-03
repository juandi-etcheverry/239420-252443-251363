using System.Security.Authentication;
using ApiModels.Requests;
using ApiModels.Responses;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Filters;
using WebApi.Filters.Logout;

namespace WebApi.Controllers;

[Route("api/purchase")]
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
    [ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
    public IActionResult GetPurchaseHistory([FromRoute] Guid userId)
    {
        User user = _userLogic.GetUser(userId);
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        var userAuth = _sessionTokenLogic.GetSessionToken(auth).User;
        if (userAuth.Role == Role.Administrador || userAuth.Id != user.Id)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var response = new GetPurchasesHistoryResponse()
            { Purchases = _purchaseLogic.GetAllPurchasesHistory(user) };
        return StatusCode(200, response);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(IsLoggedInAuthenticationFilter))]
    public IActionResult AddPurchase([FromBody] AddPurchaseRequest request)
    {
        Guid auth = Guid.Parse(Request.Cookies["Authorization"]);
        var user = _sessionTokenLogic.GetSessionToken(auth).User;
        if (user.Role == Role.Administrador)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var purchase = _purchaseLogic.AddCart(request.ToEntity());
        if (purchase.User.Id != user.Id)
            throw new InvalidCredentialException("You are not authorized to see this information");
        var response = new EffectPurchaseResponse() { Purchase = purchase };
        return StatusCode(200, response);
    }

}