using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{

    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(request.RealtorId, cancellationToken);
        if (realtor != null)
        {
            return Error.NotFound;
        }

        var owner = await unitOfWork.CustomerRepository.RetrieveAsync(request.OwnerId, cancellationToken);
        if(owner != null)
        {
            return Error.NotFound;
        }

        var address = new Address
        {
            Street = request.
        };

        var property = new Property
        {
            Title = request.Title,
            Description = request.Description,
            AddressId = address.Id,
            RealtorId = realtor.Id,
            OwnerId = owner.Id,
            Type = request.Type,
            Size = request.Size,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Price = request.Price,
            Amenities = request.Amenities,
            Status = request.Status,
            LikedByUsers = new List<User>(),
        };

        if(request.Images != null && request.Images.Any()) {
            property.Images = request.Images.Select(image => new PropertyFile(property.Id, image.FileName)).ToList();
        }

        await unitOfWork.PropertyRepository.AddAsync(property, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreatePropertyResponse
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            AddressId = address.Id,
            RealtorId = realtor.Id,
            OwnerId = owner.Id,
            Type = property.Type,
            Size = property.Size,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Price = property.Price,
            Amenities = property.Amenities,
            Status = property.Status,
            Images = property.Images
        };

        return response;
    }
}
