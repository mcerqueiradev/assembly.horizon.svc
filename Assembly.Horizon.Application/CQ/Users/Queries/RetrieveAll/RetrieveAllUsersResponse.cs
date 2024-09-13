using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Users.Queries.RetrieveAll;

public class RetrieveAllUsersResponse
{
    public IEnumerable<RetrieveUserResponse> Users { get; set; }
}
