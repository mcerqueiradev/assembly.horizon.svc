using FluentValidation;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitCommandValidator : AbstractValidator<CreatePropertyVisitCommand>
{
    public CreatePropertyVisitCommandValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty()
            .WithMessage("Property ID is required");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.RealtorId)
            .NotEmpty()
            .WithMessage("Realtor ID is required");

        RuleFor(x => x.VisitDate)
            .NotEmpty()
            .WithMessage("Visit date is required")
            .Must(BeAFutureDate)
            .WithMessage("Visit date must be in the future");

        RuleFor(x => x.TimeSlot)
            .IsInEnum()
            .WithMessage("Invalid time slot");

        When(x => x.Notes != null, () => {
            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .WithMessage("Notes cannot exceed 500 characters");
        });
    }

    private bool BeAFutureDate(DateOnly date)
    {
        return date > DateOnly.FromDateTime(DateTime.UtcNow);
    }
}