using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveByRealtor;

public class RetrieveAllPropertiesByRealtorQuery : IRequest<Result<RetrieveAllPropertiesByRealtorResponse, Success, Error>>
{
    public Guid RealtorId { get; set; }
}

