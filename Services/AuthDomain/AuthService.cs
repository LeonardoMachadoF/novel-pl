using backend.Services.AuthDomain.CreateUser;
using backend.Services.AuthDomain.Login;

namespace backend.Services.AuthDomain;

public static class AuthService
{
    public static IServiceCollection AddAuthUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        return services;
    }
}