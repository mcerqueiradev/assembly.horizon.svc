using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Notifications.Commands.MarkNotificationAsReadCommand;

public record MarkNotificationAsReadResponse(Guid NotificationId);

public record MarkNotificationAsReadCommand(Guid NotificationId)
    : IRequest<Result<MarkNotificationAsReadResponse, Success, Error>>;

public class MarkNotificationAsReadCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy)
    : IRequestHandler<MarkNotificationAsReadCommand, Result<MarkNotificationAsReadResponse, Success, Error>>
{
    public async Task<Result<MarkNotificationAsReadResponse, Success, Error>> Handle(
        MarkNotificationAsReadCommand request,
        CancellationToken cancellationToken)
    {
        var notification = await unitOfWork.NotificationRepository.RetrieveAsync(request.NotificationId);
        if (notification == null)
            return Error.NotFound;

        notification.MarkAsRead();
        await unitOfWork.CommitAsync(cancellationToken);
        await notificationStrategy.UpdateNotificationStatus(notification);

        return new MarkNotificationAsReadResponse(notification.Id);
    }
}
