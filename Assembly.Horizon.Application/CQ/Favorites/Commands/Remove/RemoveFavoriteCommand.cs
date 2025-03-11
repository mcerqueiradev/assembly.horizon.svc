using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Remove;

public record RemoveFavoriteCommand(Guid FavoriteId) : IRequest<Result<RemoveFavoriteResponse, Success, Error>>;
