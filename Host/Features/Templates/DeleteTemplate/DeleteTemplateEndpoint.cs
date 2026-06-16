using Microsoft.EntityFrameworkCore;
using TemplateApi.Data.Core;
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
        TemplateDbContext db,
        ILogger<DeleteTemplateEndpoint> logger,
        CancellationToken ct)
    {
        var entity = await db.TemplateObjects
            .FirstOrDefaultAsync(t => t.Id == id, ct)
            ?? throw new KeyNotFoundException($"Шаблонный объект с Id: {id} не найден");

        db.TemplateObjects.Remove(entity);
        await db.SaveChangesAsync(ct);

        logger.LogInformation("Шаблонный объект с Id: {Id} удалён из БД", entity.Id);

        return Results.NoContent();
    }
}
