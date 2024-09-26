using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{

    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var realtor = await unitOfWork.RealtorRepository.RetrieveByUserAsync(request.RealtorId);
        if (realtor == null)
        {
            return Error.NotFound;
        }


        var address = new Address
        {
            Id = new Guid(),
            Street = request.Street,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Reference = request.Reference
        };

        var property = new Property
        {
            Id = new Guid(),
            Title = request.Title,
            Description = request.Description,
            AddressId = address.Id,
            Address = address,
            RealtorId = realtor.Id,
            Realtor = realtor,
            Type = request.Type,
            Size = request.Size,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Price = request.Price,
            Amenities = request.Amenities,
            Status = request.Status,
            LikedByUsers = new List<User>(),
        };


        if (request.Images != null && request.Images.Any()) {
            property.Images = request.Images.Select(image => new PropertyFile(property.Id, image.FileName)).ToList();
        }

        await unitOfWork.PropertyRepository.AddAsync(property, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreatePropertyResponse
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            AddressId = property.AddressId,
            RealtorId = realtor.Id,
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

// FUNCIONA MAS PRECISA SER CORRIGIDO / MELHORADO