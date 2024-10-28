using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface ITransactionRepository : IRepository<Transaction, Guid>
{
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Transaction>> GetByContractIdAsync(Guid contractId);
    Task<Transaction> RetrieveByInvoiceIdAsync(Guid invoiceId);

}
