using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class CategoryRepository : PaginatedRepository<Category, Guid>, ICategoryRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Category> _dbSet;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Category>();
    }
}