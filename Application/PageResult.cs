namespace TemplateApi.Application;

/// <summary>
/// Модель получения списка сущностей с пагинацией
/// </summary>
/// <param name="Items">Коллекция возвращаемых элементов.</param>
/// <param name="TotalCount">Количество элементов в хранилище.</param>
public sealed record PageResult<T>(
    IReadOnlyList<T> Items,
    int TotalCount
);

