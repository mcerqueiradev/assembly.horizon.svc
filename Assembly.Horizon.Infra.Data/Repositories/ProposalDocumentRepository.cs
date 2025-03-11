using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class ProposalDocumentRepository : PaginatedRepository<ProposalDocument, Guid>, IProposalDocumentRepository
{
protected readonly ApplicationDbContext _context;
protected readonly DbSet<ProposalDocument> _dbSet;

public ProposalDocumentRepository(ApplicationDbContext context) : base(context)
{
    _context = context;
    _dbSet = context.Set<ProposalDocument>();
}
}
