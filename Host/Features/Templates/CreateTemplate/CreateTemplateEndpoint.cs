using Contracts;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Data.Core;
using TemplateApi.Host.Common;

namespace TemplateApi.Host.Features.Templates.CreateTemplate;

public sealed class CreateTemplateEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Template.TemplateObjects, Handle)
            .WithName("CreateTemplate")
            .WithTags("Templates")
            .Produces<Response>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .AddEndpointFilter<ValidationFilter<Request>>();
    }

    public record Request(string? TemplateName);

    public record Response(Guid Id, string? TemplateName);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.TemplateName).NotEmpty().MaximumLength(200);
        }
    }

    private static async Task<IResult> Handle(
        Request request,
        TemplateDbContext db,
        ILogger<CreateTemplateEndpoint> logger,
        CancellationToken ct)
    {
        var entity = TemplateObject.Create(request.TemplateName);
        db.TemplateObjects.Add(entity);
        await db.SaveChangesAsync(ct);

        logger.LogInformation("Шаблонный объект с Id: {Id} добавлен в БД", entity.Id);

        return Results.Created(ApiRoutes.Template.ForTemplateObject(entity.Id), new Response(entity.Id, entity.TemplateName));
    }
}
