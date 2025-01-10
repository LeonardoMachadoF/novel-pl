using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.NovelServices.UseCases.CreateNovel;

public interface ICreateNovelUseCase
{
    Task<Novel> Execute(CreateNovelDto novel);
}