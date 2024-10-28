using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using Assembly.Horizon.Domain.Model;

public class Comment : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
    public int? Rating { get; set; }  // Tornando o rating opcional
    public DateTime DateTimePosted { get; set; }
    public bool Flagged { get; set; }
    public int HelpfulCount { get; set; } = 0;

    private readonly List<CommentLike> _likes = new();
    public IReadOnlyCollection<CommentLike> Likes => _likes.AsReadOnly();
    public Guid? ParentCommentId { get; set; }  // ID do comentário pai (se for uma resposta)
    public Comment ParentComment { get; set; }   // Referência ao comentário pai
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();  // Coleção de respostas
    public bool IsReply => ParentCommentId.HasValue;  // Propriedade para identificar se é uma resposta

    private Comment()
    {
        Id = Guid.NewGuid();
    }

    public Comment(
        Guid id,
        Guid propertyId,
        Guid userId,
        string message,
        int? rating,
        DateTime dateTimePosted,
        bool flagged,
        Guid? parentCommentId = null)
    {
        Id = id;
        PropertyId = propertyId;
        UserId = userId;
        Message = message;
        Rating = rating;
        DateTimePosted = dateTimePosted;
        Flagged = flagged;
        ParentCommentId = parentCommentId;
    }

    public void AddLike(CommentLike like)
    {
        _likes.Add(like);
        HelpfulCount++;
    }

    public void RemoveLike(CommentLike like)
    {
        _likes.Remove(like);
        HelpfulCount--;
    }

}
