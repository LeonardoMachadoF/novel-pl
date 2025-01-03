using backend.Entities;
using backend.Services.ErrorService;
using backend.Services.NovelService;
using FluentValidation;

namespace backend.Routes;

public static class NovelRoute
{
    public static void NovelRoutes(this WebApplication app)
    {
        app.MapPost("/novel", async (NovelDTO novelDto, INovelService novelService) =>
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
        
        app.MapGet("/novel", async (INovelService novelService) =>
        {
            var novels = await novelService.GetNovels();
            return Results.Ok(novels);
        });
    }
}