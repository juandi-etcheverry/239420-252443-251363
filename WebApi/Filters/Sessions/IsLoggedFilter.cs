using System;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.Sessions
{
	public class IsLoggedFilter : Attribute, IActionFilter
    {
        private readonly ISessionTokenLogic _sessionTokenLogic;

        public IsLoggedFilter()
		{
		}
        public IsLoggedFilter(ISessionTokenLogic sessionTokenLogic)
        {
            _sessionTokenLogic = sessionTokenLogic;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                throw new UnauthorizedAccessException("You must be logged in to perform this action!");
        }
    }
}

