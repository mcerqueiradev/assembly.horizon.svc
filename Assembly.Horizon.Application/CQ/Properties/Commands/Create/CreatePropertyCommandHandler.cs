using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, INotificationStrategy notificationStrategy ) : IRequestHandler<CreatePropertyCommand, Result<CreatePropertyResponse, Success, Error>>
{
    public async Task<Result<CreatePropertyResponse, Success, Error>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var realtor = await unitOfWork.RealtorRepository.RetrieveByUserAsync(request.RealtorId);
        if (realtor == null)
        {
            return Error.NotFound;
        }

        var category = await unitOfWork.CategoryRepository.RetrieveAsync(request.CategoryId);
        if (category == null)
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
            CategoryId = category.Id,
            LikedByUsers = new List<User>(),
            Images = new List<PropertyFile>()
        };

        if (request.Images != null && request.Images.Any())
        {
            foreach (var image in request.Images)
            {
                var fileName = await fileStorageService.SaveFileAsync(image, cancellationToken);
                property.Images.Add(new PropertyFile(property.Id, fileName));
            }
        }

        var allUsers = await unitOfWork.UserRepository.RetrieveAllAsync();

        foreach (var user in allUsers)
        {
            var notification = new Notification(
                realtor.UserId,
                user.Id,
                $"New property listed: {property.Title} by {realtor.User.Name.FirstName} {realtor.User.Name.LastName}",
                NotificationType.NewListing,
                NotificationPriority.Low,
                property.Id,
                "Property"
            );

            await notificationStrategy.StoreTransientNotification(notification);
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
            Images = property.Images,
            CategoryId = property.CategoryId
        };

        return response;
    }
}
