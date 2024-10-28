using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAll;

public class RetrieveAllPropertyVisitsResponse
{
    public List<RetrievePropertyVisitResponse> Visits { get; set; }
}