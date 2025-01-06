using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.NovelServices.UseCases.UpdateNovel;

public interface IUpdateNovelUseCase
{
    Task<Novel> Execute(Guid id,UpdateNovelDto updateNovelDto);
}