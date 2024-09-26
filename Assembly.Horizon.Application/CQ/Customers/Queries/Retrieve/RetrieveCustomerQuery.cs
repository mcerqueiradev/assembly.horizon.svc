using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.Retrieve;

public class RetrieveCustomerQuery : IRequest<Result<RetrieveCustomerResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public Guid UserId { get; init; }
}
