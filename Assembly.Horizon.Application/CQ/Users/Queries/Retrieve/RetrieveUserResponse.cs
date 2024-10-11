using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserResponse
{
    public  Guid Id { get; set; }
    public  string FirstName { get; set; }
    public  string LastName { get; set; }
    public  string Email { get; set; }
    public  Access Access { get; set; }
    public  string? ImageUrl { get; set; }
    public  string? PhoneNumber { get; set; }
    public  DateTime? DateOfBirth { get; set; }
    public  bool IsActive { get; set; }
    public  DateTime? LastActiveDate { get; set; }

}
