using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.TogglePropertyActive;

public class TogglePropertyActiveCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy) : IRequestHandler<TogglePropertyActiveCommand, Result<TogglePropertyActiveResponse, Success, Error>>
{
    public async Task<Result<TogglePropertyActiveResponse, Success, Error>> Handle(
        TogglePropertyActiveCommand request,
        CancellationToken cancellationToken)
    {
        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId);

        if (property == null)
        {
            return Error.NotFound;
        }

        var wasInactive = !property.IsActive;
        property.ToggleActive();

        if (wasInactive && property.IsActive)
        {
            var allUsers = await unitOfWork.UserRepository.RetrieveAllAsync();

            foreach (var user in allUsers)
            {
                var notification = new Notification(
                    property.Realtor.UserId,
                    user.Id,
                    $"Property back in market: {property.Title} by {property.Realtor.User.Name.FirstName} {property.Realtor.User.Name.LastName}",
                    NotificationType.PropertyReactivated,
                    NotificationPriority.Low,
                    property.Id,
                    "Property"
                );

                await notificationStrategy.StoreTransientNotification(notification);
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return new TogglePropertyActiveResponse
        {
            Id = property.Id,
            IsActive = property.IsActive,
            LastActiveDate = property.LastActiveDate
        };
    }
}
