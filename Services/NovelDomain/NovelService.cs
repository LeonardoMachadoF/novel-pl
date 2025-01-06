using backend.Services.NovelDomain.UseCases.GetNovelById;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backend.Services.NovelServices.UseCases.DeleteNovel;
using backend.Services.NovelServices.UseCases.GetNovels;
using backend.Services.NovelServices.UseCases.UpdateNovel;

namespace backend.Services.NovelService;

public static class NovelService
{
    public static IServiceCollection AddNovelUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateNovelUseCase, CreateNovelUseCase>();
        services.AddScoped<IDeleteNovelUseCase, DeleteNovelUseCase>();
        services.AddScoped<IGetNovelsUseCase, GetNovelsUseCase>();
        services.AddScoped<IGetNovelUseCase, GetNovelUseCase>();
        // services.AddScoped<IGetNovelBySlugUseCase, GetNovelBySlugUseCase>();
        services.AddScoped<IUpdateNovelUseCase, UpdateNovelUseCase>();

        return services;
    }
}

