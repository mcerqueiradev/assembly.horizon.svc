using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Property : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    public Guid RealtorId { get; set; }
    public Realtor Realtor { get; set; }
    public PropertyType Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public PropertyStatus Status { get; set; }
    public List<PropertyFile> Images { get; set; } = new();
    public List<User> LikedByUsers { get; set; } = new();

    public Property() { }
    public Property(
    Guid id,
    string title,
    string description,
    Address address,
    Guid addressId,
    PropertyType type,
    double size,
    int bedrooms,
    int bathrooms,
    decimal price,
    string amenities,
    PropertyStatus status,
    List<PropertyFile> images,
    List<User> likedByUsers)
    {
        Id = id;
        Title = title;
        Description = description;
        Address = address;
        AddressId = addressId;
        Type = type;
        Size = size;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        Price = price;
        Amenities = amenities;
        Status = status;
        Images = images;
        LikedByUsers = likedByUsers;
    }
}

public enum PropertyType
{
    House,
    Apartment,
    Condo,
    Townhouse,
    Land,
    Commercial,
    Other
}
public enum PropertyStatus
{
    Available,
    Sold,
    Pending,
    Rented,
    UnderContract,
    Unavailable,
    Other
}
