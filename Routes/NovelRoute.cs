using backend.Entities;
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
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                return Results.BadRequest(new { errors });
            }
        });
        
        app.MapGet("/novel", async (INovelService novelService) =>
        {
            var novels = await novelService.GetNovels();
            return Results.Ok(novels);
        });
    }
}