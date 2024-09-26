using Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveAll;

public class RetrieveAllRealtorsResponse
{
    public IEnumerable<RetrieveRealtorResponse> Realtors { get; set; }
}