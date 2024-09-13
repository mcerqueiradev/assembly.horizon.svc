using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<Result<DeleteUserResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
