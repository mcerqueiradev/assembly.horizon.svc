using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IFavoritesRepository : IRepository<Favorites, Guid>
{
    Task<IEnumerable<Favorites>> RetrieveByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
    Task<IEnumerable<Favorites>> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> ExistsByUserAndPropertyAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken);
}
