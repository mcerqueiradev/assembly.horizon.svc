using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByUser;

public class RetrieveFavoritesByUserIdQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveFavoritesByUserIdQuery, Result<RetrieveFavoritesByUserIdResponse, Success, Error>>
{
    public async Task<Result<RetrieveFavoritesByUserIdResponse, Success, Error>> Handle(
        RetrieveFavoritesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
        var favorites = await unitOfWork.FavoritesRepository.RetrieveByUserIdAsync(request.UserId, cancellationToken);
        var propertyIds = favorites.Select(f => f.PropertyId).ToList();
        var properties = await unitOfWork.PropertyRepository.RetrieveByIdsAsync(propertyIds, cancellationToken);
        var propertiesDict = properties.ToDictionary(p => p.Id);

        var response = new RetrieveFavoritesByUserIdResponse
        {
            Favorites = favorites.Select(f => new FavoriteResponse
            {
                Id = f.Id,
                UserId = f.UserId,
                PropertyId = f.PropertyId,
                CategoryId = f.CategoryId,
                DateAdded = f.DateAdded,
                Notes = f.Notes,
                Property = propertiesDict.TryGetValue(f.PropertyId, out var property)
                        ? new RetrievePropertyResponse
                        {
                            Id = property.Id,
                            Title = property.Title,
                            Description = property.Description,
                            Street = property.Address.Street,
                            City = property.Address.City,
                            State = property.Address.State,
                            PostalCode = property.Address.PostalCode,
                            Country = property.Address.Country,
                            Reference = property.Address.Reference,
                            RealtorId = property.RealtorId,
                            Type = property.Type.ToString(),
                            Size = property.Size,
                            Bedrooms = property.Bedrooms,
                            Bathrooms = property.Bathrooms,
                            Price = property.Price,
                            Amenities = property.Amenities,
                            Status = property.Status.ToString(),
                            Images = property.Images?.Select(img => new PropertyImageResponse
                            {
                                Id = img.Id,
                                FileName = img.FileName,
                                ImagePath = $"{baseUrl}/uploads/{img.FileName}"
                            }).ToList(),
                            IsActive = property.IsActive,
                            CategoryId = property.CategoryId,
                            CategoryName = property.Category?.Name
                        }
                        : null
            })
        };

        return response;
    }
}
