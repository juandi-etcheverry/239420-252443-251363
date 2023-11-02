using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.Logout;

public class IsLoggedInAuthenticationFilter : Attribute, IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    public IsLoggedInAuthenticationFilter()
    {
    }

    public IsLoggedInAuthenticationFilter(ISessionTokenLogic sessionTokenLogic)
    {
        _sessionTokenLogic = sessionTokenLogic;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            var auth = Guid.Parse(context.HttpContext.Request.Headers["Authorization"]);
            if (_sessionTokenLogic.SessionTokenExists(auth))
            {
                if (_sessionTokenLogic.GetSessionToken(auth).User is null)
                    throw new UnauthorizedAccessException("You are not logged in");
                var sessionTokenToDelete = _sessionTokenLogic.GetSessionToken(auth);
                _sessionTokenLogic.DeleteSessionToken(sessionTokenToDelete);
                context.HttpContext.Response.Headers.Remove("Authorization");
            }
            else
            {
                throw new UnauthorizedAccessException("You are not logged in");
            }
        }
        else
        {
            throw new UnauthorizedAccessException("You are not logged in");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}