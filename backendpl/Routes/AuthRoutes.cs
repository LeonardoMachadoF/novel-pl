using System.Security.Claims;
using backend.Entities.Dto;
using backend.Services.AuthDomain.CreateUser;
using backend.Services.AuthDomain.GetUser;
using backend.Services.AuthDomain.Login;
using backend.Services.NovelService.AddToFavorite;

namespace backend.Routes;

public static class AuthRoute
{
    public static RouteGroupBuilder AuthRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (CreateUserDto createUserDto, ICreateUserUseCase createUserUseCase) =>
        {
            var user = await createUserUseCase.Execute(createUserDto);
            user.Password = "";
            return Results.Created($"/user/{user.UserId}", user);
        });

        group.MapPost("/login", async (string username, string password, ILoginUseCase loginUseCase) =>
        {
            var token = await loginUseCase.Execute(username, password);
            if (token == "")
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { token });
        });

        group.MapGet("/favorites", async (ClaimsPrincipal user, IGetUserUseCase getUserUseCase) =>
        {
            var username = user.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var infoUser = await getUserUseCase.Execute(username);
            return Results.Ok(infoUser);
        }).RequireAuthorization();

        group.MapPost("/favorites/{slug}",
            async (ClaimsPrincipal user, IAddToFavoriteUseCase addToFavoriteUseCase, string slug) =>
            {
                var username = user.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                await addToFavoriteUseCase.Execute(username, slug);
                return Results.Ok();
            }).RequireAuthorization();

        return group;
    }
}