using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Update;

public class UpdateUserResponse
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required Access Access { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ImageUrl { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
