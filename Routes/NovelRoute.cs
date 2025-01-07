using System.Security.Claims;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Services.NovelDomain.UseCases.GetNovelById;
using backend.Services.NovelService;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backend.Services.NovelServices.UseCases.DeleteNovel;
using backend.Services.NovelServices.UseCases.GetNovels;
using backend.Services.NovelServices.UseCases.UpdateNovel;
using FluentValidation;

namespace backend.Routes;

public static class NovelRoute
{
    public static RouteGroupBuilder NovelRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateNovelDto novelDto, ICreateNovelUseCase createNovelUseCase) =>
        {
            try
            {
                var novel = await createNovelUseCase.Execute(novelDto);
                return Results.Created($"/novel/{novel.Id}", novel);
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
        
        // group.MapGet("/{id}", async (IGetNovelByIdUseCase getNovelByIdUseCase, Guid id) =>
        // {
        //     try
        //     {
        //         var novels = await getNovelByIdUseCase.Execute(id);
        //         return Results.Ok(novels);
        //     }
        //     catch (Exception ex)
        //     {
        //         return Results.NotFound(new {error = ex.Message});
        //     }
        // });

        group.MapGet("/{slug}", async (ClaimsPrincipal claims, IGetNovelUseCase getNovel, string slug) =>
        {
            try
            {
                string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (Guid.TryParse(slug, out Guid novelId))
                {
                    var novel = await getNovel.Execute(novelId);
                    return Results.Ok(novel);
                }
                else
                {
                    var novel = await getNovel.Execute(slug);
                    return Results.Ok(novel);
                }
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { error = ex.Message });
            }
        }).RequireAuthorization();

        group.MapGet("/", async (IGetNovelsUseCase getNovelsUseCase, int take = 5, int skip = 0) =>
        {
            try
            {
                var novels = await getNovelsUseCase.Execute(take, skip);
                return Results.Ok(novels);
            }
            catch (ErrorCustomException ex)
            {
                return Results.BadRequest(new {error = ex.Errors});
            }
            catch (Exception ex)
            {
                return Results.NotFound(new {error = "Não foi possivel encontrar"});
            }
        });

        group.MapPut("/{slug}", async ( string slug,IUpdateNovelUseCase updateNovelUseCase,UpdateNovelDto novelUpdateDto) =>
        {
            try
            {
                var novel = await updateNovelUseCase.Execute(slug, novelUpdateDto);
                return Results.Ok(novel);
            }
            catch (ErrorCustomException ex)
            {
                return Results.BadRequest(new {error = ex.Errors});
            }
            catch (Exception ex)
            {
                return Results.NotFound(new {error = ex.Message});
            }
        });

        group.MapDelete("/{id}", async (Guid id, IDeleteNovelUseCase deleteNovelUseCase) =>
        {
            try
            {
                await deleteNovelUseCase.Execute(id);
                return Results.Ok();
            }
            catch (ErrorCustomException ex)
            {
                return Results.BadRequest(new {error = ex.Errors});
            }
        });

        return group;
    }
}