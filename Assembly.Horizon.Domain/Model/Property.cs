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
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

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
        List<User> likedByUsers,
        bool isActive,
        DateTime? lastActiveDate,
        Realtor realtor,
        Category category)
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
        IsActive = isActive;
        LastActiveDate = lastActiveDate;
        Realtor = realtor;
        RealtorId = realtor.Id;
        Category = category;
        CategoryId = category.Id;
    }

    public void ToggleActive()
    {
        IsActive = !IsActive;
        LastActiveDate = IsActive ? DateTime.UtcNow : null;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new Exception("Price must be greater than zero");
        Price = newPrice;
    }

    public void UpdateStatus(PropertyStatus newStatus)
    {
        Status = newStatus;
        if (newStatus is PropertyStatus.Sold or PropertyStatus.Rented)
        {
            IsActive = false;
            LastActiveDate = DateTime.UtcNow;
        }
    }

    public void AddImage(PropertyFile image)
    {
        Images.Add(image);
    }

    public void RemoveImage(string fileName)
    {
        var image = Images.FirstOrDefault(x => x.FileName == fileName);
        if (image != null)
            Images.Remove(image);
    }

    public void ToggleLikeByUser(User user)
    {
        if (LikedByUsers.Contains(user))
            LikedByUsers.Remove(user);
        else
            LikedByUsers.Add(user);
    }
}

public enum PropertyType
{
    Rent,
    Sale
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
