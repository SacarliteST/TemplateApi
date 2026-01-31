using Contracts;
using Contracts.Template;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Application;
using TemplateApi.Application.Services.Interface;

namespace TemplateApi.Web;

/// <summary>
/// Контроллер шаблонов
/// </summary>
[ApiController]
[TypeFilter(typeof(ExceptionMappingFilter))]
public sealed class TemplateController : ControllerBase
{
    private readonly ITemplateService templateService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TemplateController(ITemplateService templateService)
    {
        this.templateService = templateService;
    }

    /// <summary>
    /// Получения всех шаблонов с пагинацией
    /// </summary>
    [HttpGet(ApiRoutes.Template.TemplateObjects)]
    [ProducesResponseType<PageResponse<TemplateResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<PageResponse<TemplateResponse>> GetPallets(
        [FromQuery] int offset,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var pagination = Pagination.Create(offset, limit);

        var page = await templateService.GetWithPaging(pagination, cancellationToken);

        var response = new PageResponse<TemplateResponse>
        {
            Items = page.Items
                .Select(p => p.ToTemplateResponse())
                .ToList(),
            Count = page.TotalCount
        };

        return response;
    }

    /// <summary>
    /// Получение шаблона по Id
    /// </summary>
    [HttpGet(ApiRoutes.Template.TemplateObject)]
    [ProducesResponseType<TemplateResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var pallet = await templateService.GetAsync(id, cancellationToken);
        return Ok(pallet.ToTemplateResponse());
    }

    /// <summary>
    /// Создание шаблона
    /// </summary>
    [HttpPost(ApiRoutes.Template.TemplateObjects)]
    [ProducesResponseType<TemplateResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateTemplateRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCreateOrUpdateTemplateCommand();

        var pallet = await templateService.CreateAsync(command, cancellationToken);
        var response = pallet.ToTemplateResponse();

        return Created(ApiRoutes.Template.ForTemplateObject(pallet.Id), response);
    }

    /// <summary>
    /// Изменение шаблона
    /// </summary>
    [HttpPut(ApiRoutes.Template.TemplateObject)]
    [ProducesResponseType<TemplateResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] CreateOrUpdateTemplateRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCreateOrUpdateTemplateCommand();

        var updatedPallet = await templateService.UpdateAsync(id, command, cancellationToken);
        return Ok(updatedPallet.ToTemplateResponse());
    }

    /// <summary>
    /// Удаление шаблона
    /// </summary>
    [HttpDelete(ApiRoutes.Template.TemplateObjects)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await templateService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
