using Assembly.Horizon.Application.CQ.Addresses.Commands.Create;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public Guid AddressId { get; init; }
    public Guid RealtorId { get; init; }
    public Guid OwnerId { get; init; }
    public PropertyType Type { get; init; }
    public double Size { get; init; }
    public int Bedrooms { get; init; }
    public int Bathrooms { get; init; }
    public decimal Price { get; init; }
    public string Amenities { get; init; }
    public PropertyStatus Status { get; init; }
    public List<PropertyFile>? Images { get; init; }
    public int LikedByUsersCount { get; init; }
}
