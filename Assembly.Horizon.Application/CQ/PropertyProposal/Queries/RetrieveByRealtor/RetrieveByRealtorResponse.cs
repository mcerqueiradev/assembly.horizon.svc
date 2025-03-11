using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByRealtor;

public class RetrieveByRealtorResponse
{
    public IEnumerable<ProposalResponse> Proposals { get; set; }
}
