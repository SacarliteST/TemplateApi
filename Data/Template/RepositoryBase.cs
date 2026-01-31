using TemplateApi.Application;
using TemplateApi.Data.Core;

namespace TemplateApi.Data.Template;

internal abstract class RepositoryBase<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    where TKey : IEquatable<TKey>
{
    protected readonly TemplateDbContext DatabaseContext;

    protected RepositoryBase(TemplateDbContext databaseContext)
    {
        DatabaseContext = databaseContext;
    }

    public abstract Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    public Task CreateAsync(TEntity item, CancellationToken cancellationToken)
    {
        DatabaseContext.Set<TEntity>().Add(item);
        return DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(TEntity item, CancellationToken cancellationToken)
    {
        DatabaseContext.Update(item);
        return DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DatabaseContext.Set<TEntity>().Remove(entity);
        return DatabaseContext.SaveChangesAsync(cancellationToken);
    }

    public abstract Task<PageResult<TEntity>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken);
}
