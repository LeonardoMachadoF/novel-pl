using backend.Entities.Dto;
using backend.Validators;
using backendpl.Services.ValidationService;

namespace backendpl.Validators;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidationBehavior<CreateNovelDto>, ValidationBehavior<CreateNovelDto>>();
        services.AddScoped<IValidationBehavior<UpdateNovelDto>, ValidationBehavior<UpdateNovelDto>>();
        services.AddScoped<IValidationBehavior<IPagination>, ValidationBehavior<IPagination>>();
        services.AddScoped<IValidationBehavior<CreateChapterDto>, ValidationBehavior<CreateChapterDto>>();
        services.AddScoped<IValidationBehavior<UpdateChapterDto>, ValidationBehavior<UpdateChapterDto>>();
        services.AddScoped<IValidationBehavior<CreateUserDto>, ValidationBehavior<CreateUserDto>>();

        return services;
    }
}