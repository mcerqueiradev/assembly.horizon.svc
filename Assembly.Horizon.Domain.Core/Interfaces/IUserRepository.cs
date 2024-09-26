using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);

    //Task<User> UpdateAccess(Access access, CancellationToken cancellationToken);
}
