using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

internal sealed class CreateNovelValidator: AbstractValidator<CreateNovelDto>
{
    public CreateNovelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(2).WithMessage("Title must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Precisa de uma descrição")  // Portuguese message
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
    }
}