using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class RealtorRepository : PaginatedRepository<Realtor, Guid>, IRealtorRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Realtor> _dbSet;

    public RealtorRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Realtor>();
    }
}
