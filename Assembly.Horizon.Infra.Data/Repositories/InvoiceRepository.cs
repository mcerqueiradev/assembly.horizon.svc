using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class InvoiceRepository : PaginatedRepository<Invoice, Guid>, IInvoiceRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Invoice> _dbSet;

    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Invoice>();
    }
}
