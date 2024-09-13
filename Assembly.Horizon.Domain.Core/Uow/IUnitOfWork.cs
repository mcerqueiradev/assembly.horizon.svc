using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Security.Interface;
using System.Data.Common;

namespace Assembly.Horizon.Domain.Core.Uow;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public ITokenService TokenService { get; }
    public IDataProtectionService DataProtectionService { get; }
    bool Commit();

    Task<bool> CommitAsync(CancellationToken cancellationToken = default);

    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactioAsync(CancellationToken cancellationToken = default);

    Task RollbackTrasactionAsync(CancellationToken cancellationToken = default);

    bool HasChanges();

    IEnumerable<string> DebugChanges();

}
