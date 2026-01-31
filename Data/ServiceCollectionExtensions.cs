using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Application.Services;
using TemplateApi.Data.Core;
using TemplateApi.Data.Core.Configurations;
using TemplateApi.Data.Core.Migrations;
using TemplateApi.Data.Template;

namespace TemplateApi.Data;

/// <summary>
/// Добавляет методы расширения для регистрации сущностей слоя Data
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация репозиториев
    /// </summary>
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ConnectionOptions>()
            .BindConfiguration(ConnectionOptions.OptionsKey);
        services.AddDbContext<TemplateDbContext>();

        return services.AddTransient<ITemplateRepository, TemplateRepository>()
            .AddScoped<IMigrationManager, DatabaseMigrationManager>();
    }
}
