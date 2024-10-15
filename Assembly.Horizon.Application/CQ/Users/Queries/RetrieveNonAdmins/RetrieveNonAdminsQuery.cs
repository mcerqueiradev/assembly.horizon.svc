using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.RetrieveNonAdmins;

public class RetrieveNonAdminsQuery : IRequest<Result<RetrieveNonAdminsResponse, Success, Error>>
{
}
