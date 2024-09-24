using FluentValidation;

namespace Assembly.Horizon.Application.CQ.Realtors.Command.Create;

public class CreateRealtorCommandValidator : AbstractValidator<CreateRealtorCommand>
{
    public CreateRealtorCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .NotEqual(Guid.Empty).WithMessage("User ID cannot be an empty GUID.");

        RuleFor(x => x.OfficeEmail)
            .NotEmpty().WithMessage("Office email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(100).WithMessage("Office email must be less than 100 characters.");

        RuleFor(x => x.TotalSales)
            .GreaterThanOrEqualTo(0).WithMessage("Total sales cannot be negative.");

        RuleFor(x => x.TotalListings)
            .GreaterThanOrEqualTo(0).WithMessage("Total listings cannot be negative.");

        RuleFor(x => x.Certifications)
            .NotNull().WithMessage("Certifications list is required.")
            .Must(certifications => certifications.All(c => !string.IsNullOrEmpty(c)))
            .WithMessage("All certifications must be valid strings.");

        RuleFor(x => x.LanguagesSpoken)
            .NotNull().WithMessage("Languages spoken list is required.")
            .Must(languages => languages.All(l => !string.IsNullOrEmpty(l)))
            .WithMessage("All languages must be valid strings.");
    }
}
