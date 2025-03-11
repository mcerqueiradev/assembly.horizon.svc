using Assembly.Horizon.Domain.Interface;
using System;

namespace Assembly.Horizon.Domain.Model;

public class Notification : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public Guid RecipientId { get; set; }
    public User Recipient { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public DateTime ExpirationDate { get; set; }
    public NotificationPriority Priority { get; set; }
    public Guid? ReferenceId { get; set; }
    public string ReferenceType { get; set; }
    public bool IsTransient { get; set; }

    private Notification()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = NotificationStatus.Unread;
    }

    public Notification(
        Guid senderId,
        Guid recipientId,
        string message,
        NotificationType type,
        NotificationPriority priority,
        Guid? referenceId = null,
        string referenceType = null,
        bool isTransient = false) : this()
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Message = message;
        Type = type;
        Priority = priority;
        ReferenceId = referenceId;
        ReferenceType = referenceType;
        IsTransient = isTransient;
        ExpirationDate = CalculateExpirationDate(type, priority);
    }
    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
    }

    private DateTime CalculateExpirationDate(NotificationType type, NotificationPriority priority)
    {
        return type switch
        {
            NotificationType.Payment => DateTime.UtcNow.AddMonths(6),
            NotificationType.ContractUpdate => DateTime.UtcNow.AddMonths(3),
            NotificationType.PropertyUpdate => DateTime.UtcNow.AddDays(30),
            _ => DateTime.UtcNow.AddDays(7)
        };
    }
}

public enum NotificationStatus
{
    Unread,
    Read,
    Archived,
    Deleted
}

public enum NotificationType
{
    Payment,
    Contract,
    ContractUpdate,
    PropertyUpdate,
    PropertyViewing,
    NewListing,
    PriceChange,
    DocumentUpload,
    SystemAlert,
    Message,
    Favorite,
    Visit,
    Proposal,
    Negotiation,
    ProposalAccepted,
    PropertyReactivated
}

public enum NotificationPriority
{
    High,
    Medium,
    Low
}
