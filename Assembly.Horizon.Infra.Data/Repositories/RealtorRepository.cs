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

    public async Task<Realtor> RetrieveByUserAsync(Guid id)
    {
        return await _context.Realtors.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<Realtor> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(c => c.User)  // Inclui o User associado ao Customer
            .ThenInclude(u => u.Account)  // Inclui a Account associada ao User
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    public async Task<Realtor> RetrieveAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(c => c.User)  // Inclui o User associado ao Customer
            .ThenInclude(u => u.Account)  // Inclui a Account associada ao User
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

}
