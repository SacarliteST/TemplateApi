using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Data.Core;
using TemplateApi.Data.Core.Configurations;
using TemplateApi.Data.Core.Migrations;

namespace TemplateApi.Data;

/// <summary>
/// Добавляет методы расширения для регистрации сущностей слоя Data
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация инфраструктуры данных
    /// </summary>
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ConnectionOptions>()
            .BindConfiguration(ConnectionOptions.OptionsKey);

        services.AddDbContext<TemplateDbContext>();

        services.AddScoped<IMigrationManager, DatabaseMigrationManager>();

        return services;
    }
}
