using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TemplateApi.Data.Core.Configurations;

namespace TemplateApi.Data.Core;

/// <summary>
/// Шаблон контекста базы данных
/// </summary>
internal class TemplateDbContext : DbContext
{
    protected readonly ConnectionOptions Options;

    internal DbSet<TemplateObject> TemplateObjects { get; set; }

    /// <summary>
    /// Конструктор контекста
    /// </summary>
    public TemplateDbContext(IOptions<ConnectionOptions> options)
    {
        Options = options.Value;
    }

    /// <summary>
    /// Конфигурация сущностей
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IDataMarkeredInterface).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Конфигурация логирования
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Options.ConnectionString is null)
        {
            throw new InvalidOperationException("Не задана строка подключения к базе данных");
        }

        switch (Options.DbProvider)
        {
            case DbProvider.Sqlite:
                optionsBuilder.UseSqlite(Options.ConnectionString);
                break;
            case DbProvider.PostgreSql:
                optionsBuilder.UseNpgsql(Options.ConnectionString);
                break;
            default:
                throw new InvalidOperationException("Неизвестный тип провайдера базы данных");
        }
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
#endif
    }
}
