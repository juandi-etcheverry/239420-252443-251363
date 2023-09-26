using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
