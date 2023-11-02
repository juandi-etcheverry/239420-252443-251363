using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.Login;

public class LoginAuthenticationFilter : Attribute, IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    public LoginAuthenticationFilter()
    {
    }

    public LoginAuthenticationFilter(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _sessionTokenLogic = sessionTokenLogic;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            var auth = Guid.Parse(context.HttpContext.Request.Headers["Authorization"]);

            if (_sessionTokenLogic.GetSessionToken(auth).User != null)
                throw new ArgumentException("You are already logged in!");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}