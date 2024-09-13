using FluentValidation;

namespace Assembly.Horizon.Application.CQ.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User Id is required.");
    }
}