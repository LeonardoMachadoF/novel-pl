using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

public sealed class UpdateChapterValidator: AbstractValidator<UpdateChapterDto>
{
    public UpdateChapterValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.Title) || !string.IsNullOrEmpty(x.Content) || x.Number.HasValue || x.Volume.HasValue)
            .WithMessage("At least one of Title, Content, Number, or Volume must be provided.");

        // Optional validation for Title and Content (if provided)
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Content)
            .MinimumLength(20).WithMessage("Content must be at least 20 characters long.")
            .When(x => !string.IsNullOrEmpty(x.Content));

        // Optional validation for Number and Volume (if provided)
        RuleFor(x => x.Number)
            .GreaterThan(-1).WithMessage("Number must be greater than 0 if provided.");

        RuleFor(x => x.Volume)
            .GreaterThan(0).WithMessage("Volume must be greater than 0 if provided.");
    }
}