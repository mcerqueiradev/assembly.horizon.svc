using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Property.Commands.Create;


//
// CORRIGIR 
//
public class CreatePropertyResponse
{
   public required Guid Id { get; init; }
   public required string Title { get; init; }
   public required string Description { get; init; }
   public required AddressResponse Address { get; init; }
   public required RealtorResponse Realtor { get; init; }
    public required UserResponse User { get; init; }
   public required PropertyType Type { get; init; }
   public required double Size { get; init; }
   public required int Bedrooms { get; init; }
   public required int Bathrooms { get; init; }
   public required decimal Price { get; init; }
   public required string Amenities { get; init; }
   public required PropertyStatus Status { get; init; }
   public required List<PropertyFileResponse> Images { get; init; }
   public required int LikedByUsersCount { get; init; }
}

public class AddressResponse
{
   public required Guid Id { get; init; }
   public required string Street { get; init; }
   public required string City { get; init; }
   public required string State { get; init; }
   public required string PostalCode { get; init; }
   public required string Country { get; init; }
   public required string Reference { get; init; }
}

public class RealtorResponse
{
    public required Guid Id { get; init; }
    public required UserResponse User { get; init; }
    public required string OfficeEmail { get; init; }
    public required int TotalSales { get; init; }
    public required int TotalListings { get; init; }
    public required string Certifications { get; init; }
    public required List<Languages> LanguagesSpoken { get; init; }
    public required int PropertyCount { get; init; }
}

public class UserResponse
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
   public  string ImageUrl { get; init; }
}

public class PropertyFileResponse
{
   public required Guid Id { get; init; }
   public required string Url { get; init; }
   public required string Type { get; init; }
}