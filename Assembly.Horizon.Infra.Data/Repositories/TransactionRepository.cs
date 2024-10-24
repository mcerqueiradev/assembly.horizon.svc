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
}
