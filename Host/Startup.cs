using TemplateApi.Application;
using TemplateApi.Data;
using TemplateApi.Web;

namespace TemplateApi.Host;

internal static class Startup
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApplication();
        services.AddData(builder.Configuration);
        services.AddTemplateControllers();
    }

    public static ILogger CreateLogger()
    {
        using var factory = LoggerFactory.Create(options => options
            .AddConsole()
            .SetMinimumLevel(LogLevel.Trace));

        return factory.CreateLogger<Program>();
    }

    public static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}
