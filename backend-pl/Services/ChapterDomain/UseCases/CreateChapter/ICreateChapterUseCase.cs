using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.ChapterDomain.UseCases.CreateChapter;

public interface ICreateChapterUseCase
{
    Task<Chapter> Execute(CreateChapterDto createChapterDto);
}