using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByRealtor;

public class RetrieveByRealtorQuery : IRequest<Result<RetrieveByRealtorResponse, Success, Error>>
{
    public Guid RealtorId { get; set; }
}