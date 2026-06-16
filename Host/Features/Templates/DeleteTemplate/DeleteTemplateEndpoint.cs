using TemplateApi.Application.Services;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.DeleteTemplate;

public sealed class DeleteTemplateEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/pallets/{id:guid}", Handle)
            .WithName("DeleteTemplate")
            .WithTags("Templates")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> Handle(
        Guid id,
        ITemplateRepository repository,
        ILogger<DeleteTemplateEndpoint> logger,
        CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Шаблонный объект с Id: {id} не найден");

        await repository.DeleteAsync(entity, ct);

        logger.LogInformation("Шаблонный объект с Id: {Id} удалён из БД", entity.Id);

        return Results.NoContent();
    }
}
