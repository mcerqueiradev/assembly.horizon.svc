using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Update;

public class UpdateUserCommand : IRequest<Result<UpdateUserResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required Access Access { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ImageUrl { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime? LastActiveDate { get; init; }
}
