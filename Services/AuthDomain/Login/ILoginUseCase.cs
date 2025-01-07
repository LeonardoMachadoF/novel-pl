namespace backend.Services.AuthDomain.Login;

public interface ILoginUseCase
{
    Task<string> Execute(string username, string password);
}