using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Infra.Data.Context;
using Assembly.Horizon.Security.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Text;

namespace Assembly.Horizon.Infra.Data.Uow;

internal class UnitOfWork(ApplicationDbContext context, IUserRepository userRepository,
    ITokenService tokenService, IDataProtectionService dataProtectionService, IAccountRepository accountRepository) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public IUserRepository UserRepository => userRepository;
    public IAccountRepository AccountRepository => accountRepository;
    public ITokenService TokenService => tokenService;
    public IDataProtectionService DataProtectionService => dataProtectionService;

    public bool Commit()
    {
        return context.SaveChanges() > 0;
    }
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {

        return await context.SaveChangesAsync() > 0;
    }
    public IEnumerable<string> DebugChanges()
    {
        throw new NotImplementedException();
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction.GetDbTransaction();
    }
    public async Task RollbackTrasactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync(cancellationToken);
    }
    public async Task CommitTransactioAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction != null)
            {
                await context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTrasactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }
        if (disposing)
        {
            _transaction.Dispose();
            context.Dispose();
        }
        _disposed = true;
    }
    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    ~UnitOfWork()
    {
        Dispose(true);
    }
}
