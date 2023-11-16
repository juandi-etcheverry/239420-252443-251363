using System.Security.Authentication;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.User;

public class UpdateUserAuthenticationFilter : IActionFilter
{
    private readonly ISessionTokenLogic _sessionTokenLogic;

    private IUserLogic _userLogic;

    public UpdateUserAuthenticationFilter()
    {
    }

    public UpdateUserAuthenticationFilter(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
    {
        _userLogic = userLogic;
        _sessionTokenLogic = sessionTokenLogic;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            throw new UnauthorizedAccessException("You must be logged in to perform this action!");

        var auth = Guid.Parse(context.HttpContext.Request.Headers["Authorization"]);
        var session = _sessionTokenLogic.GetSessionToken(auth);

        if (session.User is null)
            throw new UnauthorizedAccessException("You must be logged in to perform this action!");

        if (session.User.Role == Role.Buyer)
        {
            var guidString = (string)context.RouteData.Values["id"];
            if (session.User.Id != Guid.Parse(guidString))
                throw new InvalidCredentialException("You can only update your own user!");
        }
    }
}