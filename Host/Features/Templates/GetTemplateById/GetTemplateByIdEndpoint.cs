using Contracts;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Data.Core;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.GetTemplateById;

public sealed class GetTemplateByIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Template.TemplateObject, Handle)
            .WithName("GetTemplateById")
            .WithTags("Templates")
            .Produces<Response>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public record Response(Guid Id, string? TemplateName);

    private static async Task<IResult> Handle(
        Guid id,
        TemplateDbContext db,
        CancellationToken ct)
    {
        var entity = await db.TemplateObjects
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, ct)
            ?? throw new KeyNotFoundException($"Шаблонный объект с Id: {id} не найден");

        return Results.Ok(new Response(entity.Id, entity.TemplateName));
    }
}
