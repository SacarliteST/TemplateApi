using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Application.Services;
using TemplateApi.Application.Services.Interface;

namespace TemplateApi.Application;

/// <summary>
/// Регистрирует сервисы
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет методы расширения для регистрации сущностей слоя Application
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddTransient<ITemplateService, TemplateService>();
    }
}
