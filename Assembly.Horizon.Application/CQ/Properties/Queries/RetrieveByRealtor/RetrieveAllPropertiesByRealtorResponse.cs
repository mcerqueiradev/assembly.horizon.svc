using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveByRealtor;

public class RetrieveAllPropertiesByRealtorResponse
{
    public IEnumerable<RetrievePropertyResponse> Properties { get; set; }
}
