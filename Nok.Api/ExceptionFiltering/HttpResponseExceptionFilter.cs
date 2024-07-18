using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Nok.Api.ExceptionFiltering;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is FluentValidation.ValidationException validationException)
        {
            context.Result = new ObjectResult(validationException.Errors)
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            context.ExceptionHandled = true;
        }
    }
}
