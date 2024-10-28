using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAll;

public class RetrieveAllPropertyVisitsQuery : IRequest<Result<RetrieveAllPropertyVisitsResponse, Success, Error>>
{
}
