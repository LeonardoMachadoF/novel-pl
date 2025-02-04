using backend.Entities.Dto;
using backend.Services.ChapterDomain.UseCases.CreateChapter;
using backend.Services.ChapterDomain.UseCases.DeleteChapter;
using backend.Services.ChapterDomain.UseCases.GetChapter;
using backend.Services.ChapterDomain.UseCases.UpdateChapter;


namespace backend.Routes;

public static class ChapterRoute
{
    public static RouteGroupBuilder ChapterRoutes(this RouteGroupBuilder group)
    {
        group.MapPost("/",
            async (string slug, CreateChapterDto chapterDto, ICreateChapterUseCase createChapterUseCase) =>
            {
                chapterDto.NovelSlug = slug;
                var chapter = await createChapterUseCase.Execute(chapterDto);
                return Results.Created($"/chapter/{chapter.Id}", chapter);
            }).RequireAuthorization("Admin");
        
        group.MapGet("/{id:guid}", async (IGetChapterByIdUseCase getChapter, Guid id) =>
        {
            var chapter = await getChapter.Execute(id);
            return chapter is not null ? Results.Ok(chapter) : Results.NotFound();
        });

        group.MapPut("/{id:guid}",
            async (IUpdateChapterUseCase updateChapterUseCase, UpdateChapterDto chapterDto, Guid id) =>
            {
                var novel = await updateChapterUseCase.Execute(chapterDto, id);
                return Results.Ok(novel);
            }).RequireAuthorization("Admin");

        group.MapDelete("/{id:guid}", async (IDeleteChapterUseCase deleteChapterUseCase, Guid id) =>
        {
            if (id == Guid.Empty) return Results.BadRequest();
            await deleteChapterUseCase.Execute(id);
            return Results.Ok();
        }).RequireAuthorization("Admin");

        return group;
    }
}