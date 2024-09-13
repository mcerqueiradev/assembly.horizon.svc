using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;
public class UserRepository : PaginatedRepository<User, Guid>, IUserRepository
{
    protected readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Account)
            .FirstOrDefaultAsync(u => u.Account.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<User>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include (u => u.Account)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.Account)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

}