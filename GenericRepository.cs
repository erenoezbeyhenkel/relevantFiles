using Azure.Core;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

/// <summary>
/// Created by our own.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class GenericRepository<TEntity>(DataBaseContext dataBaseContext) : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
{
    public IQueryable<TEntity> Query => dataBaseContext.Set<TEntity>();
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery(includes: includeExpression);
        return await query.ToListAsync(cancellationToken);
    }
    public virtual async Task<FrozenSet<TEntity>> GetAllAsFrozenSetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        var list = await GetAllAsync(includeExpression, cancellationToken);
        Guard.Against.Null(list, nameof(list));
        return list.ToFrozenSet();
    }
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsReadOnlyAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        var list = await GetAllAsync(includeExpression, cancellationToken);
        Guard.Against.Null(list, nameof(list));
        return list.ToList().AsReadOnly();
    }
    public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(filterExpression, nameof(filterExpression));
        var query = PrepareQuery(filterExpression, includeExpression);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IQueryable<TEntity>> FilterByQueryableAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        Guard.Against.Null(filterExpression, nameof(filterExpression));
        return PrepareQuery(filterExpression, includeExpression);
    }

    public virtual async Task<IEnumerable<TEntity>> FilterByEnumerableAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(filterExpression, nameof(filterExpression));
        var query = PrepareQuery(filterExpression, includeExpression);
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default) => await dataBaseContext.Set<TEntity>().Where(filterExpression).CountAsync(cancellationToken);

    /// <summary>
    ///Use this for bulk delete 
    /// </summary>
    /// <param name="filterExpression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual async Task DeleteByQueryAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(filterExpression, nameof(filterExpression));
        await dataBaseContext.Set<TEntity>().Where(filterExpression).ExecuteDeleteAsync(cancellationToken);
    }
    public virtual async Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(id, nameof(id));
        await dataBaseContext.Set<TEntity>().Where(m => m.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));
        await dataBaseContext.Set<TEntity>().Where(m => m.Id == entity.Id).ExecuteDeleteAsync(cancellationToken);
    }

    public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entities, nameof(entities));
        if (!entities.Any())
            return;

        await dataBaseContext.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entity, nameof(entity));
        await dataBaseContext.AddAsync(entity, cancellationToken);
    }

    public virtual async Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        Guard.Against.Null(entity, nameof(entity));
        dataBaseContext.Update(entity);
    }

    public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        if (Guard.Against.IsAnyOrNotEmpty(entities))
        {
            dataBaseContext.UpdateRange(entities);
        }
    }

    public virtual async Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationDescendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var pagedData = await query
        .Where(x => x.Id < cursor)
        .OrderByDescending(x => x.Id)
        .Take(pageSize)
        .ToListAsync(cancellationToken: cancellationToken);

        var nextCursor = Guard.Against.IsAnyOrNotEmpty(pagedData) ? pagedData[^1].Id : 0;

        return (pagedData, nextCursor);
    }

    public virtual async Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationDescendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var pagedData = await query
        .Where(x => x.Id < cursor && x.CreatedAt >= from && x.CreatedAt <= to)
        .OrderByDescending(x => x.Id)
        .Take(pageSize)
        .ToListAsync(cancellationToken: cancellationToken);

        var nextCursor = Guard.Against.IsAnyOrNotEmpty(pagedData) ? pagedData[^1].Id : 0;

        return (pagedData, nextCursor);
    }

    public virtual async Task<int> CursorPaginationTotalItemCountAsync(IQueryable<TEntity> query, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var pagedData = await query
        .Where(x => x.CreatedAt >= from && x.CreatedAt <= to)
        .OrderByDescending(x => x.Id)
        .ToListAsync(cancellationToken: cancellationToken);

        return pagedData.Count;
    }

    public virtual async Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationAscendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var pagedData = await query
                            .Where(x => x.Id > cursor)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken: cancellationToken);

        var nextCursor = Guard.Against.IsAnyOrNotEmpty(pagedData) ? pagedData[^1].Id : 0;

        return (pagedData, nextCursor);
    }

    public virtual async Task<IEnumerable<TEntity>> RunQueryAsnyc(IQueryable<TEntity> query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
    private IQueryable<TEntity> PrepareQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
    {
        IQueryable<TEntity> query = dataBaseContext.Set<TEntity>();

        if (!Guard.Against.IsNull(filter))
            query = query.Where(filter);

        if (!Guard.Against.IsNull(includes))
            query = includes(query);

        return query;
    }

    private bool _disposed;

    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);

        // Take this object off the finalization queue to prevent 
        // finalization code for this object from executing a second time.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <param name="disposing">Whether or not we are disposing</param> 
    /// <returns><see cref="ValueTask"/></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources.
                await dataBaseContext.DisposeAsync();
            }

            // Dispose any unmanaged resources here...

            _disposed = true;
        }
    }

}
