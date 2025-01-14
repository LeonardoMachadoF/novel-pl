using backend.Data.Enums;
using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ChapterDomain.UseCases;
using backend.Services.NovelDomain.UseCases.GetNovelById;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.ChapterTests;

public class CreateChapterUseCaseTests
{
    private readonly Mock<IChapterRepository> _chapterRepository = new();
    private readonly Mock<IValidationBehavior<CreateChapterDto>> _validation = new();
    private readonly Mock<IGetNovelUseCase> _getNovelUseCase = new();
    private readonly CreateChapterUseCase _createChapterUseCase;

    public CreateChapterUseCaseTests()
    {
        _createChapterUseCase = new CreateChapterUseCase(
            _chapterRepository.Object,
            _validation.Object,
            _getNovelUseCase.Object
        );
    }

    [Fact]
    public async Task Execute_ShouldThrowException_WhenNovelNotFound()
    {
        // Arrange
        var createChapterDto = GetCreateChapterDto("inexistente-slug");

        _getNovelUseCase.Setup(x => x.Execute(createChapterDto.NovelSlug))
            .ReturnsAsync((Novel)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _createChapterUseCase.Execute(createChapterDto));
    }

    [Fact]
    public async Task Execute_ShouldReturnChapter_WhenNovelFound()
    {
        var createChapterDto = GetCreateChapterDto("existente-slug");
        var novel = CreateNovel("existente-slug");

        _getNovelUseCase.Setup(x =>
                x.Execute(createChapterDto.NovelSlug!)
            )
            .ReturnsAsync(novel);

        var chapter = await _createChapterUseCase.Execute(createChapterDto);

        Assert.NotNull(chapter);
        Assert.Contains(chapter, novel.Chapters);
        _chapterRepository.Verify(x => x.Add(It.IsAny<Chapter>()), Times.Once);
    }

    private CreateChapterDto GetCreateChapterDto(string novelSlug) =>
        new CreateChapterDto
        {
            NovelSlug = novelSlug,
            Title = "chapter 1",
            Number = 1,
            Volume = 1,
            Content = "chapter content"
        };

    private Novel CreateNovel(string slug) =>
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