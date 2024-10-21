using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.RetrieveAll;

public class RetrieveAllContractsQuery : IRequest<Result<RetrieveAllContractsResponse, Success, Error>>
{
}
