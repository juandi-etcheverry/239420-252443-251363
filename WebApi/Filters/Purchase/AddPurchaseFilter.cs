using System.Security.Authentication;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.Purchase;

public class AddPurchaseFilter : IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    public AddPurchaseFilter()
    {
    }

    public AddPurchaseFilter(ISessionTokenLogic sessionTokenLogic)
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
            throw new InvalidCredentialException("You must be a buyer to perform this action!");
    }
}