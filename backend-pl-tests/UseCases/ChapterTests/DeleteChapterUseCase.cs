using backend.Data.Repository;
using backend.Entities;
using backend.Services.ChapterDomain.UseCases.DeleteChapter;
using Moq;

namespace backendtests.UseCases.ChapterTests;

public class DeleteChapterUseCaseTests:TestBase
{
    private readonly Mock<IChapterRepository> _chapterRepository = new ();
    
    // public async Task Execute(Guid id)
    // {
    //     var chapter = await _chapterRepository.GetChapterById(id);
    //     if (chapter is null) throw new KeyNotFoundException("Capitulo não encontrada!");
    //     await _chapterRepository.DeleteChapter(chapter);
    // }

    [Fact]
    public async Task Execute_ShouldThrowKeyNotFoundException_WhenChapterIsNotFound()
    {
        var id = Guid.NewGuid();
        _chapterRepository.Setup(x=>x.GetChapterById(id))
            .ReturnsAsync((Chapter)null);
        
        var deleteChapterUseCase = new DeleteChapterUseCase(_chapterRepository.Object);
        
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => deleteChapterUseCase.Execute(id));
        Assert.Equal("Capitulo não encontrada!", exception.Message);
    }
    
    [Fact]
    public async Task Execute_ShouldDeleteChapter_WhenChapterIsFound()
    {
        var id = Guid.NewGuid();
        var chapter = new Chapter("Chapter 1", 1, 1, "Content");
        
        _chapterRepository.Setup(x=>x.GetChapterById(id))
            .ReturnsAsync(chapter);
        
        var deleteChapterUseCase = new DeleteChapterUseCase(_chapterRepository.Object);
        
        await deleteChapterUseCase.Execute(id);
        _chapterRepository.Verify(x => x.DeleteChapter(chapter), Times.Once);
    }
    
}