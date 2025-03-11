using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByProperty;

public record RetrieveFavoritesByPropertyIdQuery(Guid PropertyId)
    : IRequest<Result<RetrieveFavoritesByPropertyIdResponse, Success, Error>>;
