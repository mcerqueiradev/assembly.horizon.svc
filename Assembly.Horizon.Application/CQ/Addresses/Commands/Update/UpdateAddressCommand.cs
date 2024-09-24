using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Addresses.Commands.Create;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Commands.Update;

public class UpdateAddressCommand : IRequest<Result<UpdateAddressResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string Reference { get; init; }
}
