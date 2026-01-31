using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Web;

/// <summary>
/// Предоставляет методы расширения сервисов из слоя Web
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет сервисы
    /// </summary>
    public static IServiceCollection AddTemplateControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add<ExceptionMappingFilter>();
                options.Filters.Add<ExceptionLoggerFilter>();
            })
            .AddApplicationPart(typeof(IWebMarkerInterface).Assembly)
            .AddControllersAsServices();
        return services;
    }
}
