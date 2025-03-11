using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Remove;

public class RemoveFavoriteCommandHandler(IUnitOfWork unitOfWork, INotificationStrategy notificationStrategy)
    : IRequestHandler<RemoveFavoriteCommand, Result<RemoveFavoriteResponse, Success, Error>>
{
    public async Task<Result<RemoveFavoriteResponse, Success, Error>> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = await unitOfWork.FavoritesRepository.RetrieveAsync(request.FavoriteId, cancellationToken);

        if (favorite is null)
            return Error.NotFound;


        await unitOfWork.FavoritesRepository.DeleteByIdAsync(favorite, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new RemoveFavoriteResponse
        {
            Id = favorite.Id,
            UserId = favorite.UserId,
            PropertyId = favorite.PropertyId,
            CategoryId = favorite.CategoryId,
            DateRemoved = DateTime.UtcNow
        };

        return response;
    }
}
