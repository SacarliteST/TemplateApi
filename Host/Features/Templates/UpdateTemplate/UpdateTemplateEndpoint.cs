using TemplateApi.Application.Services;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.UpdateTemplate;

public sealed class UpdateTemplateEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/pallets/{id:guid}", Handle)
            .WithName("UpdateTemplate")
            .WithTags("Templates")
            .Produces<Response>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public record Request(string? TemplateName);

    public record Response(Guid Id, string? TemplateName);

    private static async Task<IResult> Handle(
        Guid id,
        Request request,
        ITemplateRepository repository,
        ILogger<UpdateTemplateEndpoint> logger,
        CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Шаблонный объект с Id: {id} не найден");

        entity.Update(request.TemplateName);
        await repository.UpdateAsync(entity, ct);

        logger.LogInformation("Шаблонный объект с Id: {Id} изменён", entity.Id);

        return Results.Ok(new Response(entity.Id, entity.TemplateName));
    }
}
