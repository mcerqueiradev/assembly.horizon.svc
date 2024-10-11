using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Create;

public class CreateUserCommand : IRequest<Result<CreateUserResponse, Success, Error>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? PhoneNumber { get; init; }
    public string? ImageUrl { get; init; }
    public DateTime DateOfBirth { get; init; }
    public bool IsActive { get; init; }
    public DateTime? LastActiveDate { get; init; }
}
