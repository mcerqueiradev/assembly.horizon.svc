namespace Assembly.Horizon.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationResponse
{
    public Guid NotificationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsTransient { get; set; }
}