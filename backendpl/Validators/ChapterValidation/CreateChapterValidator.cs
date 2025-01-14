using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

public sealed class CreateChapterValidator : AbstractValidator<CreateChapterDto>
{
    public CreateChapterValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(x => x.Number)
            .GreaterThanOrEqualTo(0).WithMessage("Chapter number must be greater or equal than 0.");

        RuleFor(x => x.Volume)
            .GreaterThan(0).WithMessage("Volume must be greater than 0.");

        RuleFor(x => x.NovelSlug)
            .MinimumLength(2).WithMessage("Title must be at least 2 characters long.")
            .When(x => x.NovelSlug.Length > 0);

        RuleFor(x => x.NovelSlug)
            .NotEmpty().WithMessage("NovelSlug is required is not provided.");
    }
}