using FluentValidation;

namespace HomeInventory.Application.Features.Items.Commands.Update;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

        RuleFor(x => x.Description)
                .MaximumLength(1000);
    }
}
