using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByUser;

public class RetrieveFavoritesByUserIdQuery : IRequest<Result<RetrieveFavoritesByUserIdResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}