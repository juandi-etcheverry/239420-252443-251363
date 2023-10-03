using System.Security.Authentication;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.User;

public class UserAuthenticationFilter : Attribute, IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    private IUserLogic _userLogic;

    public UserAuthenticationFilter()
    {
    }

    public UserAuthenticationFilter(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
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
        if (!(_sessionTokenLogic.GetSessionToken(auth).User?.Role == Role.Admin ||
              _sessionTokenLogic.GetSessionToken(auth).User?.Role == Role.Total))
            throw new InvalidCredentialException("You must be an administrator to perform this action!");
    }
}