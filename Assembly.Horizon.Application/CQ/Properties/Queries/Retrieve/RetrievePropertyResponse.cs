using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Reference { get; set; }
    public Guid RealtorId { get; set; }
    public string Type { get; set; }
    public double Size { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public decimal Price { get; set; }
    public string Amenities { get; set; }
    public string Status { get; set; }
    public List<PropertyImageResponse> Images { get; set; }
    public bool IsActive { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastActiveDate { get; set; }
}
