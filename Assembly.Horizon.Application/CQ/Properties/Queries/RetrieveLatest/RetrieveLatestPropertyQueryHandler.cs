using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveLatest;

public class RetrieveLatestPropertyQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveLatestPropertyQuery, Result<RetrieveLatestPropertyResponse, Success, Error>>
{
    public async Task<Result<RetrieveLatestPropertyResponse, Success, Error>> Handle(RetrieveLatestPropertyQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var properties = await unitOfWork.PropertyRepository.RetrieveAllAsync();

        if (properties == null)
        {
            return Error.NotFound;
        }

        var lastProperty = properties.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

        if (lastProperty == null)
        {
            return Error.NotFound;
        }

        var response = new RetrieveLatestPropertyResponse
        {
            Id = lastProperty.Id,
            Title = lastProperty.Title,
            Description = lastProperty.Description,
            Street = lastProperty.Address.Street,
            City = lastProperty.Address.City,
            State = lastProperty.Address.State,
            PostalCode = lastProperty.Address.PostalCode,
            Country = lastProperty.Address.Country,
            Reference = lastProperty.Address.Reference,
            RealtorId = lastProperty.RealtorId,
            Type = lastProperty.Type,
            Size = lastProperty.Size,
            Bedrooms = lastProperty.Bedrooms,
            Bathrooms = lastProperty.Bathrooms,
            Price = lastProperty.Price,
            Amenities = lastProperty.Amenities,
            Status = lastProperty.Status,
            Images = lastProperty.Images.Select(img => new PropertyImageResponse
            {
                Id = img.Id,
                FileName = img.FileName,
                ImagePath = $"{baseUrl}/uploads/{img.FileName}"
            }).ToList(),
            IsActive = lastProperty.IsActive,
            CreatedAt = lastProperty.CreatedAt,
            LastActiveDate = lastProperty.LastActiveDate,
        };

        return response;
    }
}
