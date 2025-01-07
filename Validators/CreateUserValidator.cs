using backend.Entities.Dto;
using FluentValidation;

namespace backend.Validators;

internal sealed class CreateUserValidator:AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Usuário é obrigatório")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Usuário deve conter apenas letras, números e underscores")
            .MinimumLength(3).WithMessage("Usuário deve ter pelo menos 3 caracteres")
            .MaximumLength(20).WithMessage("Usuário deve ter no máximo 20 caracteres");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(6).WithMessage("Senha deve conter pelo menos 6 caracteres");

    }
}