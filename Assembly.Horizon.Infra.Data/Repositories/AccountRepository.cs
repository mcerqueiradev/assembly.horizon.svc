using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class AccountRepository : PaginatedRepository<Account, Guid>, IAccountRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Account> _dbSet;

    public AccountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Account>();
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        var result = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        return result;
    }
}