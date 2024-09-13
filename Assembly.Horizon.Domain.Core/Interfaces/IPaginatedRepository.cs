using Assembly.Horizon.Domain.Interface;
using Assembly.Horizon.Infra.Interface.Repositories;
using System.Linq.Expressions;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IPaginatedRepository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
{
    Task<PaginatedResult<T>> GetPaginatedAsync<TSortKey>(int pageNumber, int pageSize, Expression<Func<T, TSortKey>> sortExpression,
        Expression<Func<T, bool>>? filterExpression, bool ascending = true, CancellationToken cancellationToken = default);
}
