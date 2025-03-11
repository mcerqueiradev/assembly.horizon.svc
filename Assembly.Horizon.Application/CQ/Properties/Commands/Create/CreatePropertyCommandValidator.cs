using Assembly.Horizon.Application.CQ.Properties.Commands.Create;
using FluentValidation;

public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.Reference)
            .NotEmpty()
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.RealtorId)
            .NotEmpty();

        RuleFor(x => x.Type)
            .IsInEnum();

        RuleFor(x => x.Size)
            .GreaterThan(0);

        RuleFor(x => x.Bedrooms)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Bathrooms)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Amenities)
            .MaximumLength(int.MaxValue);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Images)
            .Must(x => x == null || x.Count <= 20)
            .WithMessage("Maximum of 10 images allowed");

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
