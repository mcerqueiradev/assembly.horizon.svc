using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Notifications.Commands.Create;

public class CreateNotificationCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy) : IRequestHandler<CreateNotificationCommand, Result<CreateNotificationResponse, Success, Error>>
{
    public async Task<Result<CreateNotificationResponse, Success, Error>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification(
                request.SenderId,
                request.RecipientId,
                request.Message,
                request.Type,
                request.Priority
            );

        if (notification.Priority == NotificationPriority.High)
        {
            await notificationStrategy.StorePersistentNotification(notification);
            await unitOfWork.NotificationRepository.AddAsync(notification);
        }
        else
        {
            await notificationStrategy.StoreTransientNotification(notification);
        }

        await unitOfWork.CommitTransactioAsync(cancellationToken);

        var response = new CreateNotificationResponse
        {
            NotificationId = notification.Id,
            CreatedAt = notification.CreatedAt,
            IsTransient = notification.Priority != NotificationPriority.High
        };

        return response;
    }
}