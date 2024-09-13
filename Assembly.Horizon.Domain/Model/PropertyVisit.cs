using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class PropertyVisit : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RealtorId { get; set; }
    public Realtor Realtor { get; set; }
    public DateTime Date { get; set; }
    public VisitStatus VisitStatus { get; set; }

    public PropertyVisit() { }

    public PropertyVisit(Guid id, Property property, Guid propertyId, User user, Guid userId, Realtor realtor, Guid realtorId, DateTime date, VisitStatus visitStatus)
    {
        Id = id;
        Property = property ?? throw new ArgumentNullException(nameof(property));
        PropertyId = propertyId;
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = userId;
        Realtor = realtor ?? throw new ArgumentNullException(nameof(realtor));
        RealtorId = realtorId;
        Date = date;
        VisitStatus = visitStatus;
    }
}

public enum VisitStatus
{
    Scheduled,
    Completed,
    Pending,
    Canceled,
    Confirmed
}
