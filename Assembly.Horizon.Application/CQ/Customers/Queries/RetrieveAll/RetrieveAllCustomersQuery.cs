using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.RetrieveAll;

public class RetrieveAllCustomersQuery : IRequest<Result<RetrieveAllCustomersResponse, Success, Error>>
{
}
