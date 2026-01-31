namespace Contracts;

/// <summary>
/// Пути к API
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Общий префикс Api
    /// </summary>
    public const string PrefixV1 = "api/v1";

    /// <summary>
    /// Задаёт URL
    /// </summary>
    public static class Template
    {
        /// <summary>
        /// Все шаблонные объекты
        /// </summary>
        public const string TemplateObjects = PrefixV1 + "/pallets";

        /// <summary>
        /// шаблонный объект по Id
        /// </summary>
        public const string TemplateObject = TemplateObjects + "/{id}";

        /// <summary>
        /// Возвращает url для страницы коробок
        /// </summary>
        public static string ForTemplateObjectPagination(int offset, int limit)
            => ReplaceUrlSegments(
                TemplateObjects + "?offset={offset}&limit={limit}",
                ("offset", offset.ToString()),
                ("limit", limit.ToString()));

        /// <summary>
        /// Создаёт ссылку на созданный шаблонный объект
        /// </summary>
        public static string ForTemplateObject(Guid id) => ReplaceUrlSegment(TemplateObject, "id", id.ToString());

        private static string ReplaceUrlSegment(string template, string name, string value)
        {
            var escapedUri = Uri.EscapeDataString(value);
            return template.Replace('{' + name + '}', escapedUri);
        }

        private static string ReplaceUrlSegments(string template, params (string Name, string Value)[] segments)
            => segments.Aggregate(template, (current, segment) => ReplaceUrlSegment(template, segment.Name, segment.Value));
    }
}
