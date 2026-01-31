using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TemplateApi.Web;

internal sealed class ExceptionMappingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var status = context.Exception.MapToStatusCode();

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = "Возникла ошибка",
            Type = context.Exception.GetType().Name,
            Detail = context.Exception.Message
        };
        context.Result = new ObjectResult(problemDetails);
        context.HttpContext.Response.StatusCode = status;
        context.ExceptionHandled = true;
    }
}
