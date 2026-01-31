namespace TemplateApi.Application;

/// <summary>
/// Набор параметров для пагинации
/// </summary>
public sealed class Pagination
{
    /// <summary>
    /// Количество элементов для пропуска
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Количество элементов
    /// </summary>
    public int Limit { get; }

    private Pagination(int offset, int limit)
    {
        Offset = offset;
        Limit = limit;
    }

    /// <summary>
    /// Создание объекта пагинации
    /// </summary>
    public static Pagination Create(int offset, int limit)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit, nameof(limit));
        ArgumentOutOfRangeException.ThrowIfLessThan(offset, 0, nameof(offset));

        return new(offset, limit);
    }
}
