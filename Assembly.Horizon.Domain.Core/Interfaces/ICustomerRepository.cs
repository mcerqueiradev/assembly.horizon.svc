using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface ICustomerRepository : IRepository<Customer, Guid>
{
    Task<Customer> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
