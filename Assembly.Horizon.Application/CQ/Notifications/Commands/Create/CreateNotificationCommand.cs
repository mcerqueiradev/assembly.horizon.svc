using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommand : IRequest<Result<CreateNotificationResponse, Success, Error>>
{
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public NotificationPriority Priority { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? ReferenceType { get; set; }
}
