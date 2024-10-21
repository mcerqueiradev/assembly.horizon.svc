using Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.RetrieveAll;

public class RetrieveAllContractsResponse
{
    public IEnumerable<RetrieveContractResponse> Contracts{ get; set; }
}