using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class UserProfileRepository : PaginatedRepository<UserProfile, Guid>, IUserProfileRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<UserProfile> _dbSet;

    public UserProfileRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<UserProfile>();
    }

    public async Task<UserProfile> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return result;
    }

    public async Task<UserProfile> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
