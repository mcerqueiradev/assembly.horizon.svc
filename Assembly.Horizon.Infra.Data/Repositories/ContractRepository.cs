using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class ContractRepository : PaginatedRepository<Contract, Guid>, IContractRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Contract> _dbSet;

    public ContractRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Contract>();
    }

    public async Task<Contract> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.Property)
                .ThenInclude(p => p.Address)
            .Include(c => c.Property)
                .ThenInclude(p => p.Images)
            .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                    .ThenInclude(u => u.Name)
            .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                    .ThenInclude(u => u.Account)
            .Include(c => c.Realtor)
                .ThenInclude(r => r.User)
                    .ThenInclude(u => u.Name)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }


}
