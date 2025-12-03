using FluentValidation;

namespace HomeInventory.Application.Features.Items.Commands.AddWarranty;

public class AddWarrantyCommandValidator : AbstractValidator<AddWarrantyCommand>
{
    public AddWarrantyCommandValidator()
    {
        RuleFor(v => v.Provider)
            .NotEmpty().WithMessage("Provider is required.")
            .MaximumLength(150);

        RuleFor(v => v.ExpirationDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
    }
}