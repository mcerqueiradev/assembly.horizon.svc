using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

namespace Assembly.Horizon.Application.CQ.Customers.Queries.Retrieve;

public class RetrieveCustomerResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public RetrieveUserResponse UserResponse { get; init; }
}