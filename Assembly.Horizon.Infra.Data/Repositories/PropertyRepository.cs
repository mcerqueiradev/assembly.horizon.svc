using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Threading;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class PropertyRepository : PaginatedRepository<Property, Guid>, IPropertyRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Property> _dbSet;

    public PropertyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Property>();
    }
    public async Task<Property> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Address)
            .Include(p => p.Images)
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }


    public async Task<List<Property>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        var properties = await DbSet
            .Include(p => p.Address)
            .Include(p => p.Images)
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return properties;
    }
}
