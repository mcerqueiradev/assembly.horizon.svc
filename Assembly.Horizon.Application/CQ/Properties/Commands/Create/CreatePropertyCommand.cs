using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    // Required basic property details
    public required string Title { get; init; }
    public required string Description { get; init; }

    // Relationships (Foreign Keys)

    public required Guid AddressId { get; init; }  // Reference to existing Address
    public required Guid RealtorId { get; init; }  // Reference to the Realtor
    public required Guid OwnerId { get; init; }    // Reference to the Owner (Customer)

    // Property details
    public required PropertyType Type { get; init; }  // Type of the property (e.g., House, Apartment, etc.)
    public required double Size { get; init; }        // Size of the property (in square meters or another unit)
    public required int Bedrooms { get; init; }       // Number of bedrooms
    public required int Bathrooms { get; init; }      // Number of bathrooms
    public required decimal Price { get; init; }      // Price of the property
    public required string Amenities { get; init; }   // Additional amenities

    // Status and meta information
    public required PropertyStatus Status { get; init; }  // Property status (e.g., For Sale, Rented)
    public List<PropertyFile>? Images { get; init; }  // List of property image files
    public int LikedByUsersCount { get; init; }  // Number of likes or favorites by users
}
