using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Addresses.Queries.RetrieveAll;

public class RetrieveAllAddressQuery : IRequest<Result<RetrieveAllAddressResponse, Success, Error>>
{
}
