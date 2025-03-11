using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class PropertyProposalRepository : PaginatedRepository<PropertyProposal, Guid>, IPropertyProposalRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<PropertyProposal> _dbSet;

    public PropertyProposalRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<PropertyProposal>();
    }
    public async Task<List<PropertyProposal>> RetrieveByPropertiesAsync(IEnumerable<Guid> propertyIds, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Property)
                .ThenInclude(property => property.Images)
            .Include(p => p.User)
                .ThenInclude(u => u.Name)
            .Include(p => p.User)
                .ThenInclude(u => u.Account)
            .Where(p => propertyIds.Contains(p.PropertyId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyProposal> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Property)
                .ThenInclude(p => p.Images)
            .Include(p => p.Property)
                .ThenInclude(p => p.Address)
            .Include(p => p.Property)
                .ThenInclude(p => p.Category)
            .Include(p => p.Property)
                .ThenInclude(p => p.Realtor)
            .Include(p => p.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<PropertyProposal>> RetrieveByUserAsync(Guid userId)
    {
        return await _dbSet
            .Include(p => p.Property)
                .ThenInclude(p => p.Images)
            .Include(p => p.Property)
                .ThenInclude(p => p.Address)
            .Include(p => p.Property)
                .ThenInclude(p => p.Category)
            .Include(p => p.Property)
                .ThenInclude(p => p.Realtor)
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(Guid proposalId, ProposalStatus status, CancellationToken cancellationToken)
    {
        var proposal = await DbSet.FindAsync(proposalId);
        Context.Entry(proposal).Property(p => p.Status).CurrentValue = status;
        Context.Entry(proposal).Property(p => p.Status).IsModified = true;
    }
}
