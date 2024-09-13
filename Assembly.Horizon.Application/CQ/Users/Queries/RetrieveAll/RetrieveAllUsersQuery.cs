using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;

public class RetrieveAllUsersQuery : IRequest<Result<RetrieveAllUsersResponse, Success, Error>>
{
}
