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
            string header = context.HttpContext.Request.Headers["Cookie"];

            if (CookieValidation.AuthExists(header))
            {
                Guid auth = CookieValidation.GetAuthFromHeader(header);

                if (_sessionTokenLogic.GetSessionToken(auth).User != null)
                {
                    context.Result = new ObjectResult(new
                    {
                        Message = "You are already logged in!"
                    })
                    {
                        StatusCode = 400
                    };
                }
            }
        }
    }
}
