using backend.Data.Enums;
using backend.Entities;
using backend.Entities.Dto;

namespace backendtests.UseCases.ChapterTests;

public abstract class TestBase
{
    protected CreateChapterDto GetCreateChapterDto(string novelSlug) =>
        new CreateChapterDto
        {
            NovelSlug = novelSlug,
            Title = "chapter 1",
            Number = 1,
            Volume = 1,
            Content = "chapter content"
        };

    protected UpdateChapterDto GetUpdateChapterDto()
    {
        return new UpdateChapterDto
        {
            Title = "chapter 1",
            Number = 2,
            Volume = 1,
            Content = "New content"
        };
    }

    protected Novel CreateNovel(string slug) =>
        new Novel(
            "titulo",
            "descricao",
            NovelOriginalLanguage.English,
            "https://coffective.com/wp-content/uploads/2018/06/default-featured-image.png.jpg"
        )
        {
            Slug = slug,
            Chapters = new List<Chapter>()
        };
}