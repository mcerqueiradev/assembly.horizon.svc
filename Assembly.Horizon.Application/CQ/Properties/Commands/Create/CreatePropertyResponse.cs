using Assembly.Horizon.Application.CQ.Addresses.Commands.Create;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AddressId { get; set; }
    public Guid RealtorId { get; set; }
    public Guid CategoryId { get; set; }
    public PropertyType Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public List<PropertyFile>? Images { get; set; }
    public int LikedByUsersCount { get; set; }
}
