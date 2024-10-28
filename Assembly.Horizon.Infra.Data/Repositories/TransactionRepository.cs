using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class TransactionRepository : PaginatedRepository<Transaction, Guid>, ITransactionRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Transaction> _dbSet;

    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Transaction>();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByContractIdAsync(Guid contractId)
    {
        return await _dbSet
            .Where(t => t.ContractId == contractId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Transaction> RetrieveByInvoiceIdAsync(Guid invoiceId)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.InvoiceId == invoiceId);
    }
}
