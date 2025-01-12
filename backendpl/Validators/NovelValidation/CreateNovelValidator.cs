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
            .NotEmpty().WithMessage("Precisa de uma descrição")
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
        RuleFor(x => x.OriginalLanguage)
            .IsInEnum().WithMessage("Invalid language selected.");
        RuleFor(x => x.ImageUrl)
            .Matches(@"^(https?://)?(www\.)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}(/[\w-]*)*").WithMessage("Invalid Image URL format.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("Invalid Image URL format.");
    }
}