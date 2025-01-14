using backend.Data.Repository;
using backend.Entities;
using backend.Services.ChapterDomain.UseCases.GetChapter;
using Moq;

namespace backendtests.UseCases.ChapterTests;

public class GetChapterByIdUseCaseTests : TestBase
{
    private readonly Mock<IChapterRepository> _chapterRepositoryMock = new();

    [Fact]
    public async Task When_chapterExists_Then_ChapterIsReturned()
    {
        var chapter = new Chapter("title", 1, 1, "content");
        _chapterRepositoryMock.Setup(x => x.GetChapterById(chapter.Id))
            .ReturnsAsync(chapter);

        var chapterUseCase = new GetChapterByIdUseCase(_chapterRepositoryMock.Object);
        var result = await chapterUseCase.Execute(chapter.Id);

        Assert.NotNull(result);
        Assert.Equal(chapter.Id, result.Id);
        Assert.Equal(chapter.Title, result.Title);
        Assert.Equal(chapter.Content, result.Content);
    }

    [Fact]
    public async Task Execute_ThrowsException_When_ChapterIsNotFound()
    {
        var chapter = new Chapter("title", 1, 1, "content");
        _chapterRepositoryMock.Setup(x => x.GetChapterById(chapter.Id))
            .ReturnsAsync((Chapter)null);

        var chapterUseCase = new GetChapterByIdUseCase(_chapterRepositoryMock.Object);

        var exception = await Assert.ThrowsAsync<Exception>(() => chapterUseCase.Execute(chapter.Id));

        Assert.Equal("Chapter not found", exception.Message);
    }
}