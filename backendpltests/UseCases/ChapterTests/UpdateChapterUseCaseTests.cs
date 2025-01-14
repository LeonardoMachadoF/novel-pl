using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ChapterDomain.UseCases.UpdateChapter;
using backendpl.Services.ValidationService;
using Moq;

namespace backendtests.UseCases.ChapterTests;

public class UpdateChapterUseCaseTests : TestBase
{
    private readonly Mock<IChapterRepository> _chapterRepository = new();
    private readonly Mock<IValidationBehavior<UpdateChapterDto>> _validationUpdate = new();

    [Fact]
    public async Task Execute_ShouldThrowException_WhenChapterIsNotFound()
    {
        var chapterId = Guid.NewGuid();
        var chapterDto = GetUpdateChapterDto();
        _chapterRepository.Setup(x => x.GetChapterById(chapterId))
            .ReturnsAsync((Chapter)null);
        var updateChapterUseCase = new UpdateChapterUseCase(_chapterRepository.Object, _validationUpdate.Object);

        var exception = await Assert.ThrowsAsync<Exception>(() => updateChapterUseCase.Execute(chapterDto, chapterId));

        Assert.Equal("Capitulo não encontrado", exception.Message);
    }

    [Fact]
    public async Task Execute_ShouldUpdateChapter_WhenChapterIsFound()
    {
        var chapterDto = GetUpdateChapterDto();
        var existingChapter = new Chapter("Old Title", 1, 1, "Old content");

        _chapterRepository.Setup(x => x.GetChapterById(existingChapter.Id))
            .ReturnsAsync(existingChapter);

        var updateChapterUseCase = new UpdateChapterUseCase(
            _chapterRepository.Object,
            _validationUpdate.Object
        );

        var updatedChapter = await updateChapterUseCase.Execute(chapterDto, existingChapter.Id);

        Assert.Equal(chapterDto.Title, updatedChapter.Title);
        Assert.Equal(chapterDto.Number, updatedChapter.Number);
        Assert.Equal(chapterDto.Volume, updatedChapter.Volume);
        Assert.Equal(chapterDto.Content, updatedChapter.Content);

        _chapterRepository.Verify(x => x.UpdateChapter(existingChapter), Times.Once);
    }
}