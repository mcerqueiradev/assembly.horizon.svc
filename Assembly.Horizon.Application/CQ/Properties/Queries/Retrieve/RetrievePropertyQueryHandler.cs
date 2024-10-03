using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrievePropertyQuery, Result<RetrievePropertyResponse, Success, Error>>
{
    public async Task<Result<RetrievePropertyResponse, Success, Error>> Handle(RetrievePropertyQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var property = await unitOfWork.PropertyRepository.RetrieveAsync(request.Id);
        
        if (property == null)
        {
            return Error.NotFound;
        }

        var response = new RetrievePropertyResponse
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
            Type = property.Type,
            Size = property.Size,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Price = property.Price,
            Amenities = property.Amenities,
            Status = property.Status,
            Images = property.Images.Select(img => new PropertyImageResponse
            {
                Id = img.Id,
                FileName = img.FileName,
                ImagePath = $"{baseUrl}/uploads/{img.FileName}"
            }).ToList(),

            IsActive = property.IsActive
        };

        return response;
    }
}