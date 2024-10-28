using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class PropertyVisit : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RealtorUserId { get; set; }
    public User RealtorUser { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public VisitStatus VisitStatus { get; set; }
    public string? Notes { get; set; }
    public string ConfirmationToken { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }

    public PropertyVisit()
    {
        GenerateConfirmationToken();
    }

    public PropertyVisit(
        Guid id,
        Property property,
        Guid propertyId,
        User user,
        Guid userId,
        User realtorUser,
        Guid realtorUserId,
        DateOnly visitDate,
        TimeSlot timeSlot,
        VisitStatus visitStatus)
    {
        Id = id;
        Property = property;
        PropertyId = propertyId;
        User = user;
        UserId = userId;
        RealtorUser = realtorUser;
        RealtorUserId = realtorUserId;
        VisitDate = visitDate;
        TimeSlot = timeSlot;
        VisitStatus = visitStatus;
        GenerateConfirmationToken();
    }

    private void GenerateConfirmationToken()
    {
        ConfirmationToken = $"VISIT-{Guid.NewGuid():N}";
    }

    public void Confirm()
    {
        VisitStatus = VisitStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        VisitStatus = VisitStatus.Canceled;
    }
}

    public enum TimeSlot
{
    Morning_8AM,
    Morning_9AM,
    Morning_10AM,
    Morning_11AM,
    Afternoon_2PM,
    Afternoon_3PM,
    Afternoon_4PM,
    Afternoon_5PM
}

public enum VisitStatus
{
    Scheduled,
    Completed,
    Pending,
    Canceled,
    Confirmed
}
