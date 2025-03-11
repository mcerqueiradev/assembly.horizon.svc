using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class ProposalNegotiationRepository : PaginatedRepository<ProposalNegotiation, Guid>, IProposalNegotiationRepository
{
protected readonly ApplicationDbContext _context;
protected readonly DbSet<ProposalNegotiation> _dbSet;

public ProposalNegotiationRepository(ApplicationDbContext context) : base(context)
{
    _context = context;
    _dbSet = context.Set<ProposalNegotiation>();
}

    public async Task<List<ProposalNegotiation>> ListByProposalAsync(Guid proposalId)
    {
        return await _dbSet
            .Include(x => x.Documents)
            .Include(x => x.Sender)
            .Where(x => x.ProposalId == proposalId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ProposalNegotiation>> ListByProposalWithSenderAsync(Guid proposalId)
    {
        return await DbSet
            .Include(n => n.Sender)
            .Include(n => n.Documents)
            .Where(n => n.ProposalId == proposalId)
            .OrderBy(n => n.CreatedAt)
            .ToListAsync();
    }
}
