using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Data.Core;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.GetAllTemplates;

public sealed class GetAllTemplatesEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Template.TemplateObjects, Handle)
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
        TemplateDbContext db,
        CancellationToken ct)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit, nameof(limit));
        ArgumentOutOfRangeException.ThrowIfLessThan(offset, 0, nameof(offset));

        var total = await db.TemplateObjects.CountAsync(ct);

        var items = await db.TemplateObjects
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(ct);

        return Results.Ok(new Response(
            items.Select(t => new TemplateItem(t.Id, t.TemplateName)).ToList(),
            total));
    }
}
