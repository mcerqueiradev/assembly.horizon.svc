using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.isFavorite;

public record IsFavoriteQuery(Guid UserId, Guid PropertyId)
    : IRequest<Result<IsFavoriteResponse, Success, Error>>;
