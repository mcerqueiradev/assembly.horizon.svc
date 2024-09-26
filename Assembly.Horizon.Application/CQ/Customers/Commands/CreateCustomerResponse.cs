using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Customers.Commands;

public class CreateCustomerResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}