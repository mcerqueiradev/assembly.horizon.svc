using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;

public class RetrieveAddressQuery : IRequest<Result<RetrieveAddressResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
