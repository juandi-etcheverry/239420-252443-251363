using System.Security.Authentication;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.Purchase;

public class GetPurchaseHistoryFilter : IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    private GetPurchaseHistoryFilter()
    {
    }

    public GetPurchaseHistoryFilter(ISessionTokenLogic sessionTokenLogic)
    {
        _sessionTokenLogic = sessionTokenLogic;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Cookies.ContainsKey("Authorization"))
            throw new UnauthorizedAccessException("You must be logged in to perform this action!");

        var auth = Guid.Parse(context.HttpContext.Request.Cookies["Authorization"]);
        var session = _sessionTokenLogic.GetSessionToken(auth);

        if (session.User is null)
            throw new UnauthorizedAccessException("You must be logged in to perform this action!");

        if (session.User.Role == Role.Admin)
            throw new InvalidCredentialException("You are an admin. You can't access to purchases");

        var guidString = (string)context.RouteData.Values["id"];
        if (session.User.Id != Guid.Parse(guidString))
            throw new InvalidCredentialException("You can only get your own purchase history!");
    }
}