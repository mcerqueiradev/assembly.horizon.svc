using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;

public record RetrieveFavoriteQuery(Guid FavoriteId) : IRequest<Result<FavoriteResponse, Success, Error>>;
