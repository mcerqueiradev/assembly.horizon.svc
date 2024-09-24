using Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Addresses.Queries.RetrieveAll;

public class RetrieveAllAddressResponse
{
    public IEnumerable<RetrieveAddressResponse> AddressResponses { get; set; }
}
