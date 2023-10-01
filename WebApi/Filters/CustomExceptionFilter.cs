using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;

namespace WebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult( new { Message = context.Exception.Message});
            }

            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = context.Exception.Message});
            }

            if (context.Exception is InvalidCredentialException)
            {
                context.Result = new ObjectResult(new { Message = context.Exception.Message})
                {
                    StatusCode = 403
                };
            }
            else
            {
                context.Result = new ObjectResult(new { Message = context.Exception.Message})
                {
                    StatusCode = 500
                };
            }
        }
    }
}
