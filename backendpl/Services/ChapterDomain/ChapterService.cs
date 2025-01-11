using backend.Services.ChapterDomain.UseCases;
using backend.Services.ChapterDomain.UseCases.CreateChapter;
using backend.Services.ChapterDomain.UseCases.DeleteChapter;
using backend.Services.ChapterDomain.UseCases.GetChapter;
using backend.Services.ChapterDomain.UseCases.UpdateChapter;

namespace backend.Services.ChapterDomain;

public static class ChapterService
{
    public static IServiceCollection AddChapterUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateChapterUseCase, CreateChapterUseCase>();
        services.AddScoped<IGetChapterByIdUseCase, GetChapterByIdUseCase>();
        services.AddScoped<IUpdateChapterUseCase, UpdateChapterUseCase>();
        services.AddScoped<IDeleteChapterUseCase, DeleteChapterUseCase>();

        return services;
    }
}

