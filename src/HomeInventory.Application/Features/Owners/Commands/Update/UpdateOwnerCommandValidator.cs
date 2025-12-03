using FluentValidation;

namespace HomeInventory.Application.Features.Owners.Commands.Update;

public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
{
    public UpdateOwnerCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();

        RuleFor(v => v.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
    }
}