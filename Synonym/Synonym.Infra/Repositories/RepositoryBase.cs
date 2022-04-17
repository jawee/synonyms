using Microsoft.EntityFrameworkCore;
using Synonym.Core.Repositories;
using Synonym.Infra.Context;

namespace Synonym.Infra.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly SynonymDbContext DbContext;

    protected RepositoryBase(SynonymDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await DbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }
}