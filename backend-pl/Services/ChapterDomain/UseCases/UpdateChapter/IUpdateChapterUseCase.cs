using backend.Entities;
using backend.Entities.Dto;

namespace backend.Services.ChapterDomain.UseCases.UpdateChapter;

public interface IUpdateChapterUseCase
{
    Task<Chapter> Execute(UpdateChapterDto chapterDto, Guid Id);
}