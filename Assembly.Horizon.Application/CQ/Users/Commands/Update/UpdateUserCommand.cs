using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Update;

public class UpdateUserCommand : IRequest<Result<UpdateUserResponse, Success, Error>>
{
    public  Guid Id { get; init; }
    public  string FirstName { get; init; }
    public  string LastName { get; init; }
    public  string Email { get; init; }
    public  string? Password { get; init; }
    public  Access? Access { get; init; }
    public  string? PhoneNumber { get; init; }
    public  string? ImageUrl { get; init; }
    public IFormFile? Upload { get; init; }
    public  DateTime DateOfBirth { get; init; }
    public  bool IsActive { get; init; }
    public  DateTime? LastActiveDate { get; init; }
}
