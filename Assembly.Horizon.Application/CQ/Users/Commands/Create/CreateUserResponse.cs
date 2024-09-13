namespace Assembly.Horizon.Application.CQ.Users.Commands.Create;

public class CreateUserResponse
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ImageUrl { get; set; }
}
