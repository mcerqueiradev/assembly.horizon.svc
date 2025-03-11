using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByProperty;

public class RetrieveFavoritesByPropertyIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveFavoritesByPropertyIdQuery, Result<RetrieveFavoritesByPropertyIdResponse, Success, Error>>
{
    public async Task<Result<RetrieveFavoritesByPropertyIdResponse, Success, Error>> Handle(
        RetrieveFavoritesByPropertyIdQuery request,
        CancellationToken cancellationToken)
    {
        var favorites = await unitOfWork.FavoritesRepository.RetrieveByPropertyIdAsync(request.PropertyId, cancellationToken);

        var response = new RetrieveFavoritesByPropertyIdResponse
        {
            Favorites = favorites.Select(f => new FavoriteResponse
            {
                Id = f.Id,
                UserId = f.UserId,
                PropertyId = f.PropertyId,
                CategoryId = f.CategoryId,
                DateAdded = f.DateAdded,
                Notes = f.Notes
            })
        };

        return response;
    }
}
