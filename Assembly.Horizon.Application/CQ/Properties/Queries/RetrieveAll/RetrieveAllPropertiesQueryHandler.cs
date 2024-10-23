using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;

public class RetrieveAllPropertiesQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveAllPropertiesQuery, Result<RetrieveAllPropertiesResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllPropertiesResponse, Success, Error>> Handle(RetrieveAllPropertiesQuery request, CancellationToken cancellationToken)
    {

            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

            var properties = await unitOfWork.PropertyRepository.RetrieveAllAsync(cancellationToken);

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
                CategoryName = property.Category.Name
            }).ToList();


            var response = new RetrieveAllPropertiesResponse { Properties = propertiesResponses };

            return response;
    }
}

public class PropertyImageResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ImagePath { get; set; }
}