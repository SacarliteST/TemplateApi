using Domain;
using TemplateApi.Application.Services;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.CreateTemplate;

public sealed class CreateTemplateEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/pallets", Handle)
            .WithName("CreateTemplate")
            .WithTags("Templates")
            .Produces<Response>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    public record Request(string? TemplateName);

    public record Response(Guid Id, string? TemplateName);

    private static async Task<IResult> Handle(
        Request request,
        ITemplateRepository repository,
        ILogger<CreateTemplateEndpoint> logger,
        CancellationToken ct)
    {
        var entity = TemplateObject.Create(request.TemplateName);
        await repository.CreateAsync(entity, ct);

        logger.LogInformation("Шаблонный объект с Id: {Id} добавлен в БД", entity.Id);

        return Results.Created($"api/v1/pallets/{entity.Id}", new Response(entity.Id, entity.TemplateName));
    }
}
