using Domain;

namespace TemplateApi.Application.Services.Interface;

/// <summary>
/// Интерфейс сервиса шаблонных объектов
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Создать шаблон
    /// </summary>
    Task<TemplateObject> CreateAsync(
        CreateOrUpdateTemplateCommand box,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить шаблон по идентификатору
    /// </summary>
    Task<TemplateObject> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить список шаблонов
    /// </summary>
    Task<PageResult<TemplateObject>> GetWithPaging(
        Pagination pagination,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Обновить шаблон
    /// </summary>
    Task<TemplateObject> UpdateAsync(
        Guid id,
        CreateOrUpdateTemplateCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить шаблон
    /// </summary>
    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
