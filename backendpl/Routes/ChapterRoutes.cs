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
            if (id == Guid.Empty)
            {
                return Results.NotFound();
            }

            var chapter = await getChapter.Execute(id);
            return Results.Ok(chapter);
        });

        group.MapPut("/{id}",
            async (IUpdateChapterUseCase updateChapterUseCase, UpdateChapterDto chapterDto, Guid id) =>
            {
                if (id == Guid.Empty) return Results.BadRequest();
                var novel = await updateChapterUseCase.Execute(chapterDto, id);
                return Results.Ok(novel);
            }).RequireAuthorization("Admin");

        group.MapDelete("/{id}", async (IDeleteChapterUseCase deleteChapterUseCase, Guid id) =>
        {
            if (id == Guid.Empty) return Results.BadRequest();
            await deleteChapterUseCase.Execute(id);
            return Results.Ok();
        }).RequireAuthorization("Admin");

        return group;
    }
}