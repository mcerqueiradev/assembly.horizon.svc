namespace Assembly.Horizon.Application.CQ.Addresses.Commands.Create;

public class CreateAddressResponse
{
    public required Guid Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string Reference { get; set; }
}
