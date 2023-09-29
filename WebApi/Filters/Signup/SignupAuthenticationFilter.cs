using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.Signup
{
    public class SignupAuthenticationFilter : Attribute, IResultFilter
    {
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public SignupAuthenticationFilter(ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid auth = Guid.Parse(context.HttpContext.Request.Headers["Authorization"]);

            if (_sessionTokenLogic.GetSessionToken(auth).User != null)
            {
                context.Result = new ObjectResult("You are already logged in!")
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }
    }
}
