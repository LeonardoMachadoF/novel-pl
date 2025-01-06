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
    public static void NovelRoutes(this WebApplication app)
    {
        app.MapPost("/novel", async (CreateNovelDto novelDto, ICreateNovelUseCase createNovelUseCase) =>
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
        
        // app.MapGet("/novel/{id}", async (IGetNovelByIdUseCase getNovelByIdUseCase, Guid id) =>
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
        
        app.MapGet("/novel/{slug}", async (IGetNovelUseCase getNovel, string slug) =>
        {
            try
            {
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
                return Results.NotFound(new {error = ex.Message});
            }
        });

        app.MapGet("/novel", async (IGetNovelsUseCase getNovelsUseCase, int take = 5, int skip = 0) =>
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

        app.MapPut("/novel/{id}", async (Guid id,IUpdateNovelUseCase updateNovelUseCase,UpdateNovelDto novelUpdateDto) =>
        {
            try
            {
                var novel = await updateNovelUseCase.Execute(id, novelUpdateDto);
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

        app.MapDelete("/novel/{id}", async (Guid id, IDeleteNovelUseCase deleteNovelUseCase) =>
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
    }
}