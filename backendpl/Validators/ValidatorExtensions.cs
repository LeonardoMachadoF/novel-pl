using backend.Services.ValidationService;

namespace backendpl.Validators;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<INovelValidationService, NovelValidationService>();
        services.AddScoped<IChapterValidationService, ChapterValidationService>();
        services.AddScoped<IUserValidationService, UserValidationService>();
        return services;
    }
}