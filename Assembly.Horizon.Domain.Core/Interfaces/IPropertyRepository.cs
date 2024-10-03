using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IPropertyRepository : IRepository<Property, Guid>
{
    Task<List<Property>> RetrieveAllAsync(CancellationToken cancellationToken = default);

    Task<Property> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
}
