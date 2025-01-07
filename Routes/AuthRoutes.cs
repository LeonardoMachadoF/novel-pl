
using backend.Entities.Dto;
using backend.Services.AuthDomain.CreateUser;
using backend.Services.AuthDomain.Login;
using backend.Services.ErrorService;


namespace backend.Routes;

public static class AuthRoute
{
    public static RouteGroupBuilder AuthRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (CreateUserDto createUserDto, ICreateUserUseCase createUserUseCase) =>
        {
            try
            {
                var user = await createUserUseCase.Execute(createUserDto);
                return Results.Created($"/novel/{user.UserId}", new { user.Email, user.Username});
            }
            catch (ErrorCustomException ex)
            {
                return Results.BadRequest(new {error = ex.Errors});
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(new {error = ex.Message});
            }
        });

        group.MapPost("login", async (string username, string password, ILoginUseCase loginUseCase) =>
        {
            var token = await loginUseCase.Execute(username, password);
            if (token == null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(token);
        });

        return group;
    }
}