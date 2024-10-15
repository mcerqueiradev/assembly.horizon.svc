using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Users.Queries.RetrieveNonAdmins;

public class RetrieveNonAdminsResponse
{
    public IEnumerable<RetrieveUserResponse> Users { get; set; }
}