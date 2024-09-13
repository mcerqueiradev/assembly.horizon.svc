using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQuery : IRequest<Result<RetrieveUserResponse, Success, Error>>
{
    public Guid Id { get; init; }
}