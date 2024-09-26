using Assembly.Horizon.Application.CQ.Customers.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.RetrieveAll;

public class RetrieveAllCustomersResponse
{
    public IEnumerable<RetrieveCustomerResponse> Customers { get; set; }
}