using FluentValidation;

namespace HomeInventory.Application.Features.Items.Commands.Create;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200); 

        RuleFor(x => x.Description)
            .MaximumLength(1000); 

        RuleFor(x => x.RoomId)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.OwnerId).NotEmpty();

    }
}