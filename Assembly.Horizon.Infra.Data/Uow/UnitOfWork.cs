using System.Data.Common;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Infra.Data.Context;
using Assembly.Horizon.Security.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace Assembly.Horizon.Infra.Data.Uow;

internal class UnitOfWork(ApplicationDbContext context, IUserRepository userRepository,
    ITokenService tokenService, 
    IDataProtectionService dataProtectionService, 
    IFileStorageService fileStorageService, 
    IFavoritesRepository favoritesRepository ,
    IAccountRepository accountRepository, 
    IPdfGenerationService pdfGenerationService,
    IContractRepository contractRepository, 
    ICustomerRepository customerRepository, 
    IAddressRepository addressRepository, 
    IRealtorRepository realtorRepository, 
    IPropertyRepository propertyRepository,
    ICategoryRepository categoryRepository,
    ITransactionRepository transactionRepository,
    IInvoiceRepository invoiceRepository,
    INotificationRepository notificationRepository,
    INotificationStrategy notificationStrategy,
    ICommentRepository commentRepository,
    IPropertyVisitRepository propertyVisitRepository,
    IProposalNegotiationRepository proposalNegotiationRepository,
    IProposalDocumentRepository proposalDocumentRepository,
    IPropertyProposalRepository propertyProposalRepository,
    IUserPostRepository userPostRepository,
    IUserProfileRepository userProfileRepository
    ) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public IUserRepository UserRepository => userRepository;
    public IAccountRepository AccountRepository => accountRepository;
    public IRealtorRepository RealtorRepository => realtorRepository;
    public ICustomerRepository CustomerRepository => customerRepository;
    public IContractRepository ContractRepository => contractRepository;
    public IPropertyRepository PropertyRepository => propertyRepository;
    public IAddressRepository AddressRepository => addressRepository;
    public IFavoritesRepository FavoritesRepository => favoritesRepository;
    public ITokenService TokenService => tokenService;
    public IDataProtectionService DataProtectionService => dataProtectionService;
    public IFileStorageService FileStorageService => fileStorageService;
    public IPdfGenerationService  PdfGenerationService => pdfGenerationService;
    public ICategoryRepository CategoryRepository => categoryRepository;
    public ITransactionRepository TransactionRepository => transactionRepository;
    public IInvoiceRepository InvoiceRepository => invoiceRepository;
    public INotificationStrategy NotificationStrategy => notificationStrategy;
    public INotificationRepository NotificationRepository => notificationRepository;
    public ICommentRepository CommentRepository => commentRepository;
    public IPropertyVisitRepository PropertyVisitRepository => propertyVisitRepository;
    public IPropertyProposalRepository PropertyProposalRepository => propertyProposalRepository;
    public IProposalDocumentRepository ProposalDocumentRepository => proposalDocumentRepository;
    public IProposalNegotiationRepository ProposalNegotiationRepository => proposalNegotiationRepository;
    public IUserProfileRepository UserProfileRepository => userProfileRepository;
    public IUserPostRepository UserPostRepository => userPostRepository;

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
