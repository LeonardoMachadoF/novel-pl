using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ChapterDomain.UseCases;
using backend.Services.ChapterDomain.UseCases.CreateChapter;
using backend.Services.ChapterDomain.UseCases.DeleteChapter;
using backend.Services.ChapterDomain.UseCases.GetChapter;
using backend.Services.ChapterDomain.UseCases.UpdateChapter;
using backend.Services.ErrorService;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backend.Services.NovelServices.UseCases.UpdateNovel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Routes;

public static class ChapterRoute
{
    public static void ChapterRoutes(this WebApplication app)
    {
        app.MapPost("/chapter", async (CreateChapterDto chapterDto, ICreateChapterUseCase createChapterUseCase) =>
        {
            try
            {
                var chapter = await createChapterUseCase.Execute(chapterDto);
                return Results.Created($"/chapter/{chapter.Id}", chapter);
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
        
        app.MapGet("/chapter/{id:guid}", async (IGetChapterByIdUseCase getChapter, Guid id) =>
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Results.NotFound();
                }
                var chapter = await getChapter.Execute(id);
                return Results.Ok(chapter);
            }
            catch (Exception ex)
            {
                return Results.NotFound(new {error = ex.Message});
            }
        });

        app.MapPut("/chapter/{id}", async (IUpdateChapterUseCase updateChapterUseCase, UpdateChapterDto chapterDto, Guid id) =>
        {
            try
            {
                if (id == Guid.Empty) return Results.BadRequest();
                var novel = await updateChapterUseCase.Execute(chapterDto, id);
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

        app.MapDelete("/chapter/{id}", async (IDeleteChapterUseCase deleteChapterUseCase, Guid id) =>
        {
            try
            {
                if (id == Guid.Empty) return Results.BadRequest();
                await deleteChapterUseCase.Execute(id);
                return Results.Ok();
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
    }
}