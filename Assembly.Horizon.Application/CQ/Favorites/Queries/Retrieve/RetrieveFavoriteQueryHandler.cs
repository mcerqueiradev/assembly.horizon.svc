using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;

public class RetrieveFavoriteQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveFavoriteQuery, Result<FavoriteResponse, Success, Error>>
{
    public async Task<Result<FavoriteResponse, Success, Error>> Handle(RetrieveFavoriteQuery request, CancellationToken cancellationToken)
    {
        var favorite = await unitOfWork.FavoritesRepository.RetrieveAsync(request.FavoriteId, cancellationToken);

        if (favorite is null)
            return Error.NotFound;

        return new FavoriteResponse
        {
            Id = favorite.Id,
            UserId = favorite.UserId,
            PropertyId = favorite.PropertyId,
            CategoryId = favorite.CategoryId,
            DateAdded = favorite.DateAdded,
            Notes = favorite.Notes
        };
    }
}