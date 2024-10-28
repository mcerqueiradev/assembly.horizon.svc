using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class CommentLike : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid CommentId { get; set; }
    public Comment Comment { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public CommentLike(Guid commentId, Guid userId)
    {
        CommentId = commentId;
        UserId = userId;
    }
}
