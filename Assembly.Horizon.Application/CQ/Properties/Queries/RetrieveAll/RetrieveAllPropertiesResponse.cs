using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;

public class RetrieveAllPropertiesResponse
{
    public IEnumerable<RetrievePropertyResponse> Properties { get; set; }
}