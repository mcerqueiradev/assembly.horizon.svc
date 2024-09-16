using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Commands.Create;

public class CreateAddressCommand : IRequest<Result<CreateAddressResponse, Success, Error>>
{
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string Reference { get; init; }
}
