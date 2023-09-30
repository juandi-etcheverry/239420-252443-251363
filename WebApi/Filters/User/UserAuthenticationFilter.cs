using ApiModels.Responses.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TypeHelper;

namespace WebApi.Filters.User
{
    public class UserAuthenticationFilter : Attribute, IActionFilter
    {

        private IUserLogic _userLogic;
        private ISessionTokenLogic _sessionTokenLogic;

        public UserAuthenticationFilter() {}

        public UserAuthenticationFilter(IUserLogic userLogic, ISessionTokenLogic sessionTokenLogic)
        {
            _userLogic = userLogic;
            _sessionTokenLogic = sessionTokenLogic;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string header = context.HttpContext.Request.Headers["Cookie"];


            if (!CookieValidation.AuthExists(header))
            {
                context.Result = new ObjectResult( new {Message = "You must be logged in to perform this action!" })
                {
                    StatusCode = 401
                };
            }
            else
            {
                Guid auth = CookieValidation.GetAuthFromHeader(header);
                if (!(_sessionTokenLogic.GetSessionToken(auth).User?.Role == Role.Administrador ||
                      _sessionTokenLogic.GetSessionToken(auth).User?.Role == Role.Total))
                {
                    context.Result = new ObjectResult(new
                    {
                        Message = "You must be an admin to perform this action!"
                    })
                    {
                        StatusCode = 403
                    };
                }
            }
        }
    }
}
