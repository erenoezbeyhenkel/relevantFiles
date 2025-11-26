using System.Collections.Frozen;
using System.Linq.Expressions;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;

/// <summary>
/// Created by EO.
/// Put all common db methods here and implement them into Generic Repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : class, new()
{
    IQueryable<TEntity> Query { get; }

    Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task<FrozenSet<TEntity>> GetAllAsFrozenSetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsReadOnlyAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task UpdateOneAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteByQueryAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default);
    Task<IQueryable<TEntity>> FilterByQueryableAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FilterByEnumerableAsync(Expression<Func<TEntity, bool>> filterExpression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression = null, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default);
    Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationDescendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, CancellationToken cancellationToken);
    Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationDescendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, DateTime from, DateTime to, CancellationToken cancellationToken);
    Task<(IEnumerable<TEntity>, long nextCursor)> CursorPaginationAscendingAsync(IQueryable<TEntity> query, int pageSize, long cursor, CancellationToken cancellationToken);
    Task<int> CursorPaginationTotalItemCountAsync(IQueryable<TEntity> query, DateTime from, DateTime to, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> RunQueryAsnyc(IQueryable<TEntity> query, CancellationToken cancellationToken);
}
