namespace Assembly.Horizon.Application.CQ.Comments.Commands.Create;

public class CreateCommentResponse
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
}