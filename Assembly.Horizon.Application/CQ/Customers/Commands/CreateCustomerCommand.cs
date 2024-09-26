using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Commands;

public class CreateCustomerCommand : IRequest<Result<CreateCustomerResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
}