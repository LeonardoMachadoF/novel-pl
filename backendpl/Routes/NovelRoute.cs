using backend.Entities.Dto;
using backend.Services.NovelDomain.UseCases.GetNovelById;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backend.Services.NovelServices.UseCases.DeleteNovel;
using backend.Services.NovelServices.UseCases.GetNovels;
using backend.Services.NovelServices.UseCases.UpdateNovel;


namespace backend.Routes;

public static class NovelRoute
{
    public static RouteGroupBuilder NovelRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/",
            async (CreateNovelDto novelDto, ICreateNovelUseCase createNovelUseCase) =>
            {
                var novel = await createNovelUseCase.Execute(novelDto);
                return Results.Created($"/novel/{novel.Id}", novel);
            });
        
        group.MapGet("/{slug}", async (IGetNovelUseCase getNovel, string slug) =>
        {
            var novel = Guid.TryParse(slug, out Guid novelId)
                ? await getNovel.Execute(novelId)
                : await getNovel.Execute(slug);

            return Results.Ok(novel);
        });

        group.MapGet("/", async (IGetNovelsUseCase getNovelsUseCase, int take = 5, int skip = 0) =>
        {
            var novels = await getNovelsUseCase.Execute(take, skip);
            return Results.Ok(novels);
        });

        group.MapPut("/{slug}",
            async (string slug, IUpdateNovelUseCase updateNovelUseCase, UpdateNovelDto novelUpdateDto) =>
            {
                var novel = await updateNovelUseCase.Execute(slug, novelUpdateDto);
                return Results.Ok(novel);
            });

        group.MapDelete("/{id}", async (Guid id, IDeleteNovelUseCase deleteNovelUseCase) =>
        {
            await deleteNovelUseCase.Execute(id);
            return Results.Ok();
        });

        return group;
    }
}