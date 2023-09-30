using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.Login
{
    public class LoginAuthenticationFilter : Attribute, IActionFilter
    {
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public LoginAuthenticationFilter() {}

        public LoginAuthenticationFilter(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }

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

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
