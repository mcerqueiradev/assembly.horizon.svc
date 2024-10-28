using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class PropertyVisitRepository : PaginatedRepository<PropertyVisit, Guid>, IPropertyVisitRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<PropertyVisit> _dbSet;

    public PropertyVisitRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<PropertyVisit>();
    }

    public async Task<IEnumerable<PropertyVisit>> RetrieveAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Property)
            .Include(pv => pv.User)
            .Include(pv => pv.RealtorUser)
            .Where(pv => pv.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyVisit> RetrieveByUserIdSingleAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Property)
            .Include(pv => pv.User)
            .Include(pv => pv.RealtorUser)
            .FirstOrDefaultAsync(pv => pv.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<PropertyVisit>> RetrieveAllAsync(CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Include(v => v.Property)
            .Include(v => v.User)
            .Include(v => v.RealtorUser)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyVisit>> GetVisitsByDateAndProperty(Guid propertyId, string date, CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Where(visit =>
                visit.PropertyId == propertyId &&
                visit.VisitDate.ToString() == date &&
                visit.VisitStatus != VisitStatus.Canceled)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyVisit>> RetrieveAllByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Include(v => v.Property)
            .Include(v => v.User)
                .ThenInclude(u => u.Name)
            .Include(v => v.RealtorUser)
                .ThenInclude(u => u.Name)
            .Where(v => v.UserId == userId || v.RealtorUserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyVisit> RetrieveByTokenAsync(string token)
    {
        return await _dbSet
            .Include(v => v.Property)
            .Include(v => v.User)
                .ThenInclude(u => u.Name)
            .Include(v => v.User)
                .ThenInclude(u => u.Account)
            .Include(v => v.RealtorUser)
                .ThenInclude(r => r.Name)
            .Include(v => v.RealtorUser)
                .ThenInclude(r => r.Account)
            .FirstOrDefaultAsync(v => v.ConfirmationToken == token);
    }

}
