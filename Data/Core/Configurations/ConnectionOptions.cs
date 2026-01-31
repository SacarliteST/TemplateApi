namespace TemplateApi.Data.Core.Configurations;

internal sealed class ConnectionOptions
{
    /// <summary>
    /// Ключ параметров
    /// </summary>
    public const string OptionsKey = "ConnectionStrings";

    /// <summary>
    /// Провайдер БД
    /// </summary>
    public required DbProvider DbProvider { get; set; }

    /// <summary>
    /// Строка подключения
    /// </summary>
    public string? ConnectionString { get; set; }
}
