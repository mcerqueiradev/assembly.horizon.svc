using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Create;

public class CreateFavoriteCommand : IRequest<Result<CreateFavoriteResponse, Success, Error>>
{
    public Guid UserId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid CategoryId { get; set; }
    public string Notes { get; set; }
}
