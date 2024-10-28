using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<IEnumerable<Comment>> GetCommentsByPropertyId(Guid propertyId);
    Task<CommentLike> GetCommentLikeAsync(Guid commentId, Guid userId, CancellationToken cancellationToken);
    Task AddCommentLikeAsync(CommentLike like, CancellationToken cancellationToken);
    Task RemoveCommentLikeAsync(CommentLike like, CancellationToken cancellationToken);
}
