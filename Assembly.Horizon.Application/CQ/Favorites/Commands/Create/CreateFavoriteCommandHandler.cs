using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Create;

public class CreateFavoriteCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy) : IRequestHandler<CreateFavoriteCommand, Result<CreateFavoriteResponse, Success, Error>>
{
    public async Task<Result<CreateFavoriteResponse, Success, Error>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = new Domain.Model.Favorites
        {
            UserId = request.UserId,
            PropertyId = request.PropertyId,
            CategoryId = request.CategoryId,
            DateAdded = DateTime.UtcNow,
            Notes = "N/A",
        };

        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.PropertyId, cancellationToken);
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(property.RealtorId, cancellationToken);
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);

        var notification = new Notification(
            user.Id, 
            realtor.UserId, 
            $"{user.Name.FirstName } {user.Name.LastName} added {property.Title} to favorites",
            NotificationType.Favorite,
            NotificationPriority.Low,
            property.Id,
            "Property"
        );

        await unitOfWork.FavoritesRepository.AddAsync(favorite, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        await notificationStrategy.StoreTransientNotification(notification);

        var response = new CreateFavoriteResponse
        {
            Id = favorite.Id,
            UserId = favorite.UserId,
            PropertyId = favorite.PropertyId,
            CategoryId = favorite.CategoryId,
            DateAdded = favorite.DateAdded,
            Notes = favorite.Notes
        };

        return response;
    }
}
