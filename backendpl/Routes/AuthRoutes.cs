using System.Security.Claims;
using System.Text.Json;
using backend.Entities.Dto;
using backend.Services.AuthDomain.CreateUser;
using backend.Services.AuthDomain.GetUser;
using backend.Services.AuthDomain.Login;
using backend.Services.NovelService.AddToFavorite;
using Microsoft.AspNetCore.Mvc;

namespace backend.Routes;

public static class AuthRoute
{
    public static RouteGroupBuilder AuthRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/register", async (CreateUserDto createUserDto, ICreateUserUseCase createUserUseCase) =>
        {
            var user = await createUserUseCase.Execute(createUserDto);
            var userResponse = new
            {
                user.UserId,
                user.Username,
                user.Email
            };

            return Results.Created($"/user/{user.UserId}", userResponse);
        });

        group.MapPost("/login", async (UserLoginDto userLoginDto, ILoginUseCase loginUseCase) =>
        {
            var token = await loginUseCase.Execute(userLoginDto.username, userLoginDto.password);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { token });
        });

        group.MapGet("/favorites", async (ClaimsPrincipal user, IGetUserUseCase getUserUseCase) =>
        {
            var username = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Results.Unauthorized();
            }
            var infoUser = await getUserUseCase.Execute(username);
            var userResponse = new
            {
                infoUser.UserId,
                infoUser.Username,
                infoUser.Email
            };

            return Results.Ok(userResponse);
        }).RequireAuthorization("User");

        group.MapPost("/favorites/{slug}",
            async (ClaimsPrincipal user, IAddToFavoriteUseCase addToFavoriteUseCase, string slug) =>
            {
                var username = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return Results.Unauthorized();
                }
                await addToFavoriteUseCase.Execute(username, slug);
                return Results.Ok();
            }).RequireAuthorization("User");

        return group;
    }
}