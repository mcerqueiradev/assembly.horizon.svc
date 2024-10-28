using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class CommentRepository : PaginatedRepository<Comment, Guid>, ICommentRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Comment> _dbSet;
    protected readonly DbSet<CommentLike> _likeDbSet;

    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Comment>();
        _likeDbSet = context.Set<CommentLike>();
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPropertyId(Guid propertyId)
    {
        return await _dbSet
            .Where(comment => comment.PropertyId == propertyId)
            .ToListAsync();
    }

    public async Task<CommentLike> GetCommentLikeAsync(Guid commentId, Guid userId, CancellationToken cancellationToken)
    {
        return await _likeDbSet
            .FirstOrDefaultAsync(like =>
                like.CommentId == commentId &&
                like.UserId == userId,
                cancellationToken);
    }

    public async Task AddCommentLikeAsync(CommentLike like, CancellationToken cancellationToken)
    {
        await _likeDbSet.AddAsync(like, cancellationToken);
    }

    public Task RemoveCommentLikeAsync(CommentLike like, CancellationToken cancellationToken)
    {
        _likeDbSet.Remove(like);
        return Task.CompletedTask;
    }
}
