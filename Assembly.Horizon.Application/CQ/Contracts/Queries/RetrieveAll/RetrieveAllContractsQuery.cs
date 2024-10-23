using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.RetrieveAll;

public class RetrieveAllContractsQuery : IRequest<Result<RetrieveAllContractsResponse, Success, Error>>
{
}
