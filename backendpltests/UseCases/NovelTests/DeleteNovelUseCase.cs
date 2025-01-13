using backend.Data.Enums;
using backend.Data.Repository;
using backend.Entities;
using backend.Services.NovelServices.UseCases.DeleteNovel;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class DeleteNovelUseCaseTests : NovelBaseTest
{
    private readonly DeleteNovelUseCase _deleteNovelUseCase;

    public DeleteNovelUseCaseTests() => _deleteNovelUseCase = new DeleteNovelUseCase(_novelRepository.Object);
    
    [Fact]
    public async Task Execute_ShouldThrowException_WhenIdNotFound()
    {
        var id = Guid.NewGuid();
        _novelRepository
            .Setup(x => x.GetNovelById(id))
            .ReturnsAsync((Novel)null);

        var exception = await Assert.ThrowsAsync<Exception>(() => _deleteNovelUseCase.Execute(id));

        Assert.NotNull(exception);
        Assert.Equal("Novel não encontrada", exception.Message);
    }

    [Fact]
    public async Task Execute_ShouldNotThrowException_WhenValidId()
    {
        var novel = new Novel(
            "valid title",
            "valid description",
            NovelOriginalLanguage.English,
            "default.jpg"
        );

        _novelRepository
            .Setup(x => x.GetNovelById(novel.Id))
            .ReturnsAsync(novel);

        await _deleteNovelUseCase.Execute(novel.Id);
    }
}