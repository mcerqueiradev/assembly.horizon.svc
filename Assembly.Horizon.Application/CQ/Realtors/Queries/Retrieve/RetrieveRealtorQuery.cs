using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;

public class RetrieveRealtorQuery : IRequest<Result<RetrieveRealtorResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
