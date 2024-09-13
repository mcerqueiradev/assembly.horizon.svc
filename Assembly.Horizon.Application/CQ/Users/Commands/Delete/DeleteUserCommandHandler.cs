using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<DeleteUserResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeleteUserResponse, Success, Error>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.RetrieveAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Error.NotFound;
        }

        await _unitOfWork.UserRepository.DeleteByIdAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new DeleteUserResponse { Id = request.Id };
    }
}
