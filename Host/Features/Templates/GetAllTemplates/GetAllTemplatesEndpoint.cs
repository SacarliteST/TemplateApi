using Microsoft.AspNetCore.Mvc;
using TemplateApi.Application;
using TemplateApi.Application.Services;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.GetAllTemplates;

public sealed class GetAllTemplatesEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/pallets", Handle)
            .WithName("GetAllTemplates")
            .WithTags("Templates")
            .Produces<Response>()
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    public record Response(IReadOnlyList<TemplateItem> Items, int Count);

    public record TemplateItem(Guid Id, string? TemplateName);

    private static async Task<IResult> Handle(
        [FromQuery] int offset,
        [FromQuery] int limit,
        ITemplateRepository repository,
        CancellationToken ct)
    {
        var pagination = Pagination.Create(offset, limit);
        var page = await repository.GetAllAsync(pagination, ct);

        var response = new Response(
            page.Items.Select(t => new TemplateItem(t.Id, t.TemplateName)).ToList(),
            page.TotalCount);

        return Results.Ok(response);
    }
}
