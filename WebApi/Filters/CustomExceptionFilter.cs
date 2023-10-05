using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentException)
            context.Result = new BadRequestObjectResult(new { context.Exception.Message });

        else if (context.Exception is UnauthorizedAccessException)
            context.Result = new UnauthorizedObjectResult(new { context.Exception.Message });

        else if (context.Exception is InvalidCredentialException)
            context.Result = new ObjectResult(new { context.Exception.Message })
            {
                StatusCode = 403
            };
        else
            context.Result = new ObjectResult(new { context.Exception.Message })
            {
                StatusCode = 500
            };
    }
}