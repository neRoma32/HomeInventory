using FluentValidation;

namespace HomeInventory.Application.Features.Owners.Commands.Create;

public class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerCommandValidator()
    {
        RuleFor(v => v.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);
    }
}