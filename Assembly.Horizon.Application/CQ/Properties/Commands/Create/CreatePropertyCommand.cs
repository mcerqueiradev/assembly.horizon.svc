using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.Create;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{
    public  string Title { get; init; }
    public  string Description { get; init; }


    // ADDRESS
    public  string Street { get; init; }
    public  string City { get; init; }
    public  string State { get; init; }
    public  string PostalCode { get; init; }
    public  string Country { get; init; }
    public  string Reference { get; init; }

    // REALTOR 
    public  Guid RealtorId { get; init; }

    public  PropertyType Type { get; init; }
    public  double Size { get; init; }
    public  int Bedrooms { get; init; }
    public  int Bathrooms { get; init; }
    public  decimal Price { get; init; }
    public  string Amenities { get; init; }

    public  PropertyStatus Status { get; init; }

    public string FileName { get; init; }
    public List<PropertyFile>? Images { get; init; }
    public int LikedByUsersCount { get; init; }
}
