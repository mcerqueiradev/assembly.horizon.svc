using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IAddressRepository : IRepository<Address, Guid>
{
    Task<Address> GetFullAddressAsync(Address address, CancellationToken cancellationToken = default);
}
