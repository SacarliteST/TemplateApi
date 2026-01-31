using Domain;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Application;
using TemplateApi.Application.Services;
using TemplateApi.Data.Core;

namespace TemplateApi.Data.Template;

internal sealed class TemplateRepository : RepositoryBase<TemplateObject, Guid>, ITemplateRepository
{
    public TemplateRepository(TemplateDbContext databaseContext)
        : base(databaseContext)
    {
    }

    public override async Task<PageResult<TemplateObject>> GetAllAsync(
        Pagination pagination,
        CancellationToken cancellationToken)
    {
        var itemCount = await DatabaseContext.TemplateObjects.CountAsync(cancellationToken);

        var itemList = await DatabaseContext.TemplateObjects
            .OrderBy(p => p.Id)
            .Skip(pagination.Offset)
            .Take(pagination.Limit)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new(itemList, itemCount);
    }

    public override Task<TemplateObject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return DatabaseContext.TemplateObjects
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
