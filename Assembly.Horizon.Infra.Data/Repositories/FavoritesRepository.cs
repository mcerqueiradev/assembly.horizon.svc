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
}