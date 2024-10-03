using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyQuery : IRequest<Result<RetrievePropertyResponse, Success, Error>>
{
    public Guid Id { get; init; }
}