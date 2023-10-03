using Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Logic.Interfaces;
namespace WebApi.Filters;

public class CartAuthenticationFilter : IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;
    private readonly IPurchaseLogic _purchaseLogic;
    
    public CartAuthenticationFilter()
    {
    }
    
    public CartAuthenticationFilter(ISessionTokenLogic sessionTokenLogic, IPurchaseLogic purchaseLogic)
    {
        _sessionTokenLogic = sessionTokenLogic;
        _purchaseLogic = purchaseLogic;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.HttpContext.Request.Cookies.ContainsKey("Authorization"))
        {
            Guid auth = Guid.Parse(context.HttpContext.Request.Cookies["Authorization"]);
            SessionToken session = _sessionTokenLogic.GetSessionToken(auth);
            if(session.Cart == null)
            {
                Purchase newPurchase = new Purchase();
                _purchaseLogic.AddCart(newPurchase);
                _sessionTokenLogic.UpdateCart(auth, newPurchase);
            }
        }
        else
        {
            SessionToken newSession = new SessionToken
            {
                Cart = new Purchase()
            };
            _sessionTokenLogic.AddSessionToken(newSession);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}