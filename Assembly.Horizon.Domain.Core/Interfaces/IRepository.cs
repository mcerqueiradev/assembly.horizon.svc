using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Infra.Interface.Repositories;

public interface IRepository<T, TId> where T : class, IEntity<TId>
{
    Task<IEnumerable<T>> RetrieveAllAsync(CancellationToken cancellationToken = default);
    Task<T> RetrieveAsync(TId id, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> DeleteByIdAsync(T entity, CancellationToken cancellationToken = default);
}

