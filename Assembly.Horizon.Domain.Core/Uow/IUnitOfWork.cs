using System.Data.Common;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Security.Interface;

namespace Assembly.Horizon.Domain.Core.Uow;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public IRealtorRepository RealtorRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IContractRepository ContractRepository { get; }
    public IPropertyRepository PropertyRepository { get; }
    public IAddressRepository AddressRepository { get; }
    public ITokenService TokenService { get; }
    public IDataProtectionService DataProtectionService { get; }
    public IFileStorageService FileStorageService { get; }
    public IPdfGenerationService PdfGenerationService { get; }
    public IFavoritesRepository FavoritesRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ITransactionRepository TransactionRepository { get; }
    public IInvoiceRepository InvoiceRepository { get; }
    public INotificationStrategy NotificationStrategy { get; }
    public INotificationRepository NotificationRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public IPropertyVisitRepository PropertyVisitRepository{ get; }
    public IProposalDocumentRepository ProposalDocumentRepository { get; }
    public IProposalNegotiationRepository ProposalNegotiationRepository { get; }
    public IPropertyProposalRepository PropertyProposalRepository { get; }
    public IUserProfileRepository UserProfileRepository { get; }
    public IUserPostRepository UserPostRepository { get; }
    bool Commit();

    Task<bool> CommitAsync(CancellationToken cancellationToken = default);

    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactioAsync(CancellationToken cancellationToken = default);

    Task RollbackTrasactionAsync(CancellationToken cancellationToken = default);

    bool HasChanges();

    IEnumerable<string> DebugChanges();

}
