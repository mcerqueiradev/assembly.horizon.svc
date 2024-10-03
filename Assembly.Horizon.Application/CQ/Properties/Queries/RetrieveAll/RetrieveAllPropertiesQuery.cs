using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;


public class RetrieveAllPropertiesQuery : IRequest<Result<RetrieveAllPropertiesResponse, Success, Error>>
{

}
