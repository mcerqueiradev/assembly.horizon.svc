using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class UserPostRepository : PaginatedRepository<UserPost, Guid>, IUserPostRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<UserPost> _dbSet;

    public UserPostRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<UserPost>();
    }

    public async Task<IEnumerable<UserPost>> GetPostsByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(x => x.UserId == userId && x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserPost>> GetLatestPostsAsync(int take = 20)
    {
        return await _dbSet
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.CreatedAt)
            .Take(take)
            .ToListAsync();
    }

    public async Task<bool> HasUserLikedPostAsync(Guid userId, Guid postId)
    {
        return await _context.Set<UserPostLike>()
            .AnyAsync(x => x.UserId == userId && x.PostId == postId);
    }

    public async Task<bool> HasUserSharedPostAsync(Guid userId, Guid postId)
    {
        return await _context.Set<UserPostShare>()
            .AnyAsync(x => x.UserId == userId && x.PostId == postId);
    }

    public async Task<int> GetPostLikesCountAsync(Guid postId)
    {
        return await _context.Set<UserPostLike>()
            .CountAsync(x => x.PostId == postId);
    }

    public async Task<int> GetPostSharesCountAsync(Guid postId)
    {
        return await _context.Set<UserPostShare>()
            .CountAsync(x => x.PostId == postId);
    }
}
