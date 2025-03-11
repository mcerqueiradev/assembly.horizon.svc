using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;


public class RetrieveAllPropertiesQuery : IRequest<Result<RetrieveAllPropertiesResponse, Success, Error>>
{

}
