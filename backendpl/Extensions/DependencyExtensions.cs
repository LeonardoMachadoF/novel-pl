using backend.Data.Repository;
using backend.Middlewares;
using backend.Services.AuthDomain;
using backend.Services.ChapterDomain;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using backend.Utils;
using backendpl.Validators;

public static class DependencyExtensions
{
    public static IServiceCollection AddDependencyInjectionsService(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddScoped<INovelRepository, NovelRepository>();
        services.AddScoped<IChapterRepository, ChapterRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IErrorService, ErrorService>();
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        
        services.AddAuthUseCases();
        services.AddNovelUseCases();
        services.AddChapterUseCases();
        services.AddValidators();
        return services;
    }
}