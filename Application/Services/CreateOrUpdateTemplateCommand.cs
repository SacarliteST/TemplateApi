namespace TemplateApi.Application.Services;

/// <summary>
/// Пример запроса на создание шаблона
/// </summary>
/// <param name="TemplateName">Имя шаблона</param>
public sealed record CreateOrUpdateTemplateCommand(
 string TemplateName);
