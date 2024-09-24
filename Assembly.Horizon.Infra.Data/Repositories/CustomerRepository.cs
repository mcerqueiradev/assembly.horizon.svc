using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class CustomerRepository : PaginatedRepository<Customer, Guid>, ICustomerRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Customer> _dbSet;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Customer>();
    }
}
