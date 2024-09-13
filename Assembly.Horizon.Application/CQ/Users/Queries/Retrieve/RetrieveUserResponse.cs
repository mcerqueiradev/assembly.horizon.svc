using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserResponse
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required Access Access { get; set; }
    public required string ImageUrl { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime? LastActiveDate { get; set; }

}
