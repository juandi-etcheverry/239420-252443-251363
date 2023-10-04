using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.Logout
{
    public class IsLoggedInAuthenticationFilter : Attribute, IActionFilter
    {

        private readonly ISessionTokenLogic _sessionTokenLogic;

        public IsLoggedInAuthenticationFilter() {}
        public IsLoggedInAuthenticationFilter(ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Authorization"))
            {
                var auth = Guid.Parse(context.HttpContext.Request.Cookies["Authorization"]);
                if (_sessionTokenLogic.SessionTokenExists(auth))
                {
                    if (_sessionTokenLogic.GetSessionToken(auth).User is null)
                    {
                        throw new UnauthorizedAccessException("You are not logged in");
                    }
                    SessionToken sessionTokenToDelete = _sessionTokenLogic.GetSessionToken(auth);
                    _sessionTokenLogic.DeleteSessionToken(sessionTokenToDelete);
                    context.HttpContext.Response.Cookies.Delete("Authorization");
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

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
