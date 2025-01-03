using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

internal sealed class UpdateNovelDtoValidator:AbstractValidator<NovelUpdateDto>
{
    public UpdateNovelDtoValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(2).WithMessage("Title must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.")
            .When(x => x.Title.Length > 0);
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.")
            .When(x => x.Title.Length > 0);
        
        RuleFor(x=>x)
            .Must(x => !string.IsNullOrEmpty(x.Title) || !string.IsNullOrEmpty(x.Description))
            .WithMessage("Pelo menos um dos campos deve estar preenchido.");
    }
}