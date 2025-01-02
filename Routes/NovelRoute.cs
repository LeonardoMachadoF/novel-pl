using backend.Entities;
using backend.Services.NovelService;
using FluentValidation;

namespace backend.Routes;

public static class NovelRoute
{
    public static void NovelRoutes(this WebApplication app)
    {
        app.MapPost("/novel", async (NovelDTO novelDto, INovelService novelService, IValidator<NovelDTO> validator) =>
        {
            var  validationResult = validator.Validate(novelDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            var novel = await novelService.CreateNovel(novelDto);
            return Results.Created($"/novel/{novel.Id}", novel);
        });
        
        app.MapGet("/novel", async (INovelService novelService) =>
        {
            var novels = await novelService.GetNovels();
            return Results.Ok(novels);
        });
    }
}