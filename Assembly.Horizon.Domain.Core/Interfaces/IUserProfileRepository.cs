using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IUserProfileRepository : IRepository<UserProfile, Guid>
{
    Task<UserProfile> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

}
