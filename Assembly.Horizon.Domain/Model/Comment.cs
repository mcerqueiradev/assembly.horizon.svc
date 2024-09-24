using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Comment : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; }
    public DateTime DateTimePosted { get; set; }
    public bool Flagged { get; set; }

    private Comment()
    {
        Id = Guid.NewGuid();
    }
    public Comment(
        Guid id,
        Guid propertyId,
        Guid userId,
        string message,
        int rating,
        DateTime dateTimePosted,
        bool flagged
        )
    {
        Id = id;
        PropertyId = propertyId;
        UserId = userId;
        Message = message;
        Rating = rating;
        DateTimePosted = dateTimePosted;
        Flagged = flagged;

    }
}

