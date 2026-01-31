using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TemplateApi.Web;

internal sealed class ExceptionLoggerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionMappingFilter> logger;

    public ExceptionLoggerFilter(ILogger<ExceptionMappingFilter> logger)
    {
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception.MapToStatusCode() == 500)
        {
            logger.LogError(context.Exception, "Возникла критическая ошибка:");
        }
    }
}
