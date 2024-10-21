using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveLatest;

public class RetrieveLatestPropertyQuery : IRequest<Result<RetrieveLatestPropertyResponse, Success, Error>>
{
}
