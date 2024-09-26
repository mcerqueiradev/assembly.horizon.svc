using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveAll;

public class RetrieveAllRealtorsQuery : IRequest<Result<RetrieveAllRealtorsResponse, Success, Error>>
{
}
