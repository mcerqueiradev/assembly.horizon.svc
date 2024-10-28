namespace Assembly.Horizon.Application.CQ.Comments.Queries.RetrieveByProperty;

public class RetrieveCommentsByPropertyResponse
{
    public List<CommentDto> Comments { get; set; } = new();
}

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public int? Rating { get; set; }
    public DateTime DateTimePosted { get; set; }
    public bool Flagged { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string UserCommentName { get; set; }
    public string? UserCommentPhoto { get; set; }
    public string UserCommentEmail { get; set; }
    public int HelpfulCount { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
}

