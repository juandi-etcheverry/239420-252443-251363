using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.Signup
{
    public class SignupAuthenticationFilter : Attribute, IActionFilter
    {
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public SignupAuthenticationFilter() { }
        public SignupAuthenticationFilter(ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Authorization"))
            {
                Guid auth = Guid.Parse(context.HttpContext.Request.Cookies["Authorization"]);
                if (_sessionTokenLogic.GetSessionToken(auth).User != null)
                {
                    throw new ArgumentException("You are already logged in!");
                }
            }
        }
    }
}
