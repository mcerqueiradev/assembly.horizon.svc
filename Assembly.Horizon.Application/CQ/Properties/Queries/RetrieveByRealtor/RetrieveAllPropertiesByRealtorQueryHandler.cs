using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveByRealtor;

public class RetrieveAllPropertiesByRealtorQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveAllPropertiesByRealtorQuery, Result<RetrieveAllPropertiesByRealtorResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllPropertiesByRealtorResponse, Success, Error>> Handle(
        RetrieveAllPropertiesByRealtorQuery request,
        CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var properties = await unitOfWork.PropertyRepository.RetrieveByRealtorAsync(request.RealtorId, cancellationToken);

        if (properties == null || !properties.Any())
            return Error.NotFound;

        var propertiesResponses = properties.Select(property => new RetrievePropertyResponse
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
            CategoryId = property.CategoryId,
            Type = property.Type.ToString(),
            Size = property.Size,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Price = property.Price,
            Amenities = property.Amenities,
            Status = property.Status.ToString(),
            Images = property.Images.Select(img => new PropertyImageResponse
            {
                Id = img.Id,
                FileName = img.FileName,
                ImagePath = $"{baseUrl}/uploads/{img.FileName}"
            }).ToList(),
            IsActive = property.IsActive,
            CategoryName = property.Category.Name,
            CreatedAt = property.CreatedAt,
            LastActiveDate = property.LastActiveDate,
        }).ToList();

        var response = new RetrieveAllPropertiesByRealtorResponse { Properties = propertiesResponses };

        return response;
    }
}
