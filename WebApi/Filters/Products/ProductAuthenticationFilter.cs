using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;
using TypeHelper;

namespace WebApi.Filters.Products
{
    public class ProductAuthenticationFilter : IActionFilter
    {
        private readonly ISessionTokenLogic _sessionTokenLogic;
        public ProductAuthenticationFilter() { }

        public ProductAuthenticationFilter(ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Authorization"))
            {
                var auth = Guid.Parse(context.HttpContext.Request.Cookies["Authorization"]);
                if (_sessionTokenLogic.SessionTokenExists(auth))
                {
                    SessionToken sessionToken = _sessionTokenLogic.GetSessionToken(auth);
                    if (sessionToken.User is null)
                    {
                        throw new UnauthorizedAccessException("You are not logged in");
                    }

                    if (!(sessionToken.User?.Role == Role.Administrador ||
                          sessionToken.User?.Role == Role.Total))
                        throw new InvalidCredentialException("You must be an administrator to perform this action!");
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
    }
}
