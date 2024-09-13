using FluentValidation;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Auth
{
    public class AuthUserValidator : AbstractValidator<AuthUserQuery>
    {
        public AuthUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
