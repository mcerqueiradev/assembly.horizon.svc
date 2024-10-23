using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Favorites.Commands.Create;

public class CreateFavoriteCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateFavoriteCommand, Result<CreateFavoriteResponse, Success, Error>>
{
    public async Task<Result<CreateFavoriteResponse, Success, Error>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = new Domain.Model.Favorites
        {
            UserId = request.UserId,
            PropertyId = request.PropertyId,
            CategoryId = request.CategoryId,
            DateAdded = DateTime.UtcNow,
            Notes = "N/A",
        };

        await unitOfWork.FavoritesRepository.AddAsync(favorite, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateFavoriteResponse
        {
            Id = favorite.Id,
            UserId = favorite.UserId,
            PropertyId = favorite.PropertyId,
            CategoryId = favorite.CategoryId,
            DateAdded = favorite.DateAdded,
            Notes = favorite.Notes
        };

        return response;
    }
}
