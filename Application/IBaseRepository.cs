namespace TemplateApi.Application;

/// <summary>
/// Базовый репозиторий
/// </summary>
public interface IBaseRepository<TEntity, TKey>
{
    /// <summary>
    /// Получение всех элементов склада
    /// </summary>
    Task<PageResult<TEntity>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление сущности в хранилище
    /// </summary>
    Task CreateAsync(TEntity item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление сущности в хранилище
    /// </summary>
    Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение сущности по ключу
    /// </summary>
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
