using Assembly.Horizon.Domain.Interface;
using System;

namespace Assembly.Horizon.Domain.Model
{
    public class Notification : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Realtor SenderUser { get; set; }
        public Guid RecipientId { get; set; }
        public Customer RecipientUser { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public NotificationPriority Priority { get; set; }

        private Notification()
        {
            Id = Guid.NewGuid();
        }

        public Notification(
            Guid senderId,
            Guid recipientId,
            string message,
            DateTime date,
            NotificationStatus status,
            NotificationType type,
            DateTime expirationDate,
            NotificationPriority priority)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Message = message;
            Date = date;
            Status = status;
            Type = type;
            ExpirationDate = expirationDate;
            Priority = priority;
        }
    }

    public enum NotificationStatus
    {
        Unread,
        Read,
        Acknowledged,
        Pending
    }

    public enum NotificationType
    {
        Alert,
        Message,
        Update,
        Payment
    }

    public enum NotificationPriority
    {
        High,
        Medium,
        Low
    }
}
