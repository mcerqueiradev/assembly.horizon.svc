using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class FavoritesRepository : PaginatedRepository<Favorites, Guid>, IFavoritesRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Favorites> _dbSet;

    public FavoritesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Favorites>();
    }

    public async Task<IEnumerable<Favorites>> RetrieveByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken)
    {
        return await _context.Favorites
            .Where(f => f.PropertyId == propertyId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Favorites>> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Favorites
            .Where(f => f.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByUserAndPropertyAsync(Guid userId, Guid propertyId, CancellationToken cancellationToken)
    {
        return await _context.Favorites
            .AsNoTracking()
            .AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId, cancellationToken);
    }
}