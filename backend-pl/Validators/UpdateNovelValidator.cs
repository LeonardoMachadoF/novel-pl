using backend.Data.Enums;
using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

internal sealed class UpdateNovelValidator:AbstractValidator<UpdateNovelDto>
{
    public UpdateNovelValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(2).WithMessage("Title must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.")
            .When(x => x.Title.Length > 0);
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.")
            .When(x => x.Title.Length > 0);
        RuleFor(x => x.OriginalLanguage)
            .IsInEnum().WithMessage("Invalid language selected.");
        RuleFor(x => x.ImageUrl)
            .Matches(@"^(https?://)?(www\.)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}(/[\w-]*)*").WithMessage("Invalid Image URL format.")
            .When(x => !string.IsNullOrEmpty(x.ImageUrl))
            .WithMessage("Invalid Image URL format.");
        
        RuleFor(x=>x)
            .Must(x => !string.IsNullOrEmpty(x.Title) || !string.IsNullOrEmpty(x.Description) || !string.IsNullOrEmpty(x.ImageUrl) || Enum.IsDefined(typeof(NovelOriginalLanguage), x.OriginalLanguage))
            .WithMessage("Pelo menos um dos campos deve estar preenchido.");

    }
}