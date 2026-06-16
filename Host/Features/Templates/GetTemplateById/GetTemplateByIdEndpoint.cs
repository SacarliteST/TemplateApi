using TemplateApi.Application.Services;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.GetTemplateById;

public sealed class GetTemplateByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/pallets/{id:guid}", Handle)
            .WithName("GetTemplateById")
            .WithTags("Templates")
            .Produces<Response>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public record Response(Guid Id, string? TemplateName);

    private static async Task<IResult> Handle(
        Guid id,
        ITemplateRepository repository,
        CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Шаблонный объект с Id: {id} не найден");

        return Results.Ok(new Response(entity.Id, entity.TemplateName));
    }
}
