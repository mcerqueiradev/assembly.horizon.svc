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
}
