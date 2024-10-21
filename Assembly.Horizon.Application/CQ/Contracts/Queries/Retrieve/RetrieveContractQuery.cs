using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;

public class RetrieveContractQuery : IRequest<Result<RetrieveContractResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
