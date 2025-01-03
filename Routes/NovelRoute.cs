using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using FluentValidation;

namespace backend.Routes;

public static class NovelRoute
{
    public static void NovelRoutes(this WebApplication app)
    {
        app.MapPost("/novel", async (NovelDto novelDto, INovelService novelService) =>
        {
            try
            {
                var novel = await novelService.CreateNovel(novelDto);
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
        
        app.MapGet("/novel/{id}", async (INovelService novelService, Guid id) =>
        {
            try
            {
                var novels = await novelService.GetNovelById(id);
                return Results.Ok(novels);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new {error = ex.Message});
            }
        });

        app.MapGet("/novel", async (INovelService novelService, int take = 5, int skip = 0) =>
        {
            try
            {
                var novels = await novelService.GetNovels(take, skip);
                return Results.Ok(novels);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new {error = "Não foi possivel encontrar"});
            }
        });

        app.MapPut("/novel/{id}", async (Guid id,INovelService novelService,NovelUpdateDto novelUpdateDto) =>
        {
            try
            {
                var novel = await novelService.UpdateNovel(id, novelUpdateDto);
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

        app.MapDelete("/novel/{id}", async (Guid id, INovelService novelService) =>
        {
            try
            {
                await novelService.DeleteNovel(id);
                return Results.Ok();
            }
            catch (ErrorCustomException ex)
            {
                return Results.BadRequest(new {error = ex.Errors});
            }
        });
    }
}