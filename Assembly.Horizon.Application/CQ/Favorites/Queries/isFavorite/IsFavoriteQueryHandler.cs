using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.isFavorite;

public class IsFavoriteQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<IsFavoriteQuery, Result<IsFavoriteResponse, Success, Error>>
{
    public async Task<Result<IsFavoriteResponse, Success, Error>> Handle(
        IsFavoriteQuery request,
        CancellationToken cancellationToken)
    {
        var isFavorite = await unitOfWork.FavoritesRepository
            .ExistsByUserAndPropertyAsync(request.UserId, request.PropertyId, cancellationToken);

        return new IsFavoriteResponse(isFavorite);
    }
}
