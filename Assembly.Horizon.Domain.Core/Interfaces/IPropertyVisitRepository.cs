using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IPropertyVisitRepository : IRepository<PropertyVisit, Guid>
{
    Task<IEnumerable<PropertyVisit>> RetrieveAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<PropertyVisit> RetrieveByUserIdSingleAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyVisit>> GetVisitsByDateAndProperty(Guid propertyId, string date, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyVisit>> RetrieveAllByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<PropertyVisit> RetrieveByTokenAsync(string token);
}
