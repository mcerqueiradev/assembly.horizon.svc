using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Address : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Reference { get; set; }
    private Address()
    {
        Id = Guid.NewGuid();
    }
    public Address(Guid id, string street, string city, string state, string postalCode, string country, string reference)
    {
        Id = id;
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        Reference = reference;
    }

}