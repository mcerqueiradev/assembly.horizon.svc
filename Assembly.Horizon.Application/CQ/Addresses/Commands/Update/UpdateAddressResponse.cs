namespace Assembly.Horizon.Application.CQ.Addresses.Commands.Update;

public class UpdateAddressResponse
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Reference { get; set; }
}
