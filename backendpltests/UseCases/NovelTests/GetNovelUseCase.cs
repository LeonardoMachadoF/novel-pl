using backend.Data.Enums;
using backend.Data.Repository;
using backend.Entities;
using backend.Services.NovelDomain.UseCases.GetNovel;
using backend.Services.NovelServices.UseCases.GetNovels;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class GetNovelUseCaseTests:NovelBaseTest
{

    private readonly GetNovelUseCase _getNovelUseCase;
    public GetNovelUseCaseTests()=>_getNovelUseCase = new GetNovelUseCase(_novelRepository.Object);
    
    [Fact]
    public async Task Execute_ShouldReturnNovel_WhenValidId()
    {
        var novel = new Novel(
            "valid title",
            "valid description",
            NovelOriginalLanguage.English,
            "default.jpg"
        );
        _novelRepository.Setup(m => m.GetNovelById(novel.Id)).ReturnsAsync(novel);
        
        var result = await _getNovelUseCase.Execute(novel.Id);
        
        Assert.NotNull(result);
        Assert.Equal(novel, result);
    }
    
    [Fact]
    public async Task Execute_ShouldReturnNovel_WhenValidSlug()
    {
        var novel = new Novel(
            "valid title",
            "valid description",
            NovelOriginalLanguage.English,
            "default.jpg"
        );
        
        _novelRepository.Setup(m => m.GetNovelBySlugWithChapters(novel.Slug)).ReturnsAsync(novel);
        
        var result = await _getNovelUseCase.Execute(novel.Slug);
        
        Assert.NotNull(result);
        Assert.Equal(novel, result);
    }
    
    [Fact]
    public async Task Execute_ShouldTrhowException_WhenInvalidSlug()
    {
        var novel = new Novel(
            "valid title",
            "valid description",
            NovelOriginalLanguage.English,
            "default.jpg"
        );
        
        _novelRepository
            .Setup(m => m.GetNovelBySlugWithChapters(novel.Slug))
            .ReturnsAsync((Novel)null);
        
       var exception = await Assert.ThrowsAsync<Exception>(() => _getNovelUseCase.Execute(novel.Slug));
        
        Assert.NotNull(exception);
        Assert.Equal("Novel não encontrada5", exception.Message);
    }
    [Fact]
    public async Task Execute_ShouldTrhowException_WhenInvalidId()
    {
        var novel = new Novel(
            "valid title",
            "valid description",
            NovelOriginalLanguage.English,
            "default.jpg"
        );
        
        _novelRepository
            .Setup(m => m.GetNovelById(novel.Id))
            .ReturnsAsync((Novel)null);
        
        var exception = await Assert.ThrowsAsync<Exception>(() => _getNovelUseCase.Execute(novel.Id));
        
        Assert.NotNull(exception);
        Assert.Equal("Novel não encontrada4", exception.Message);
    }
}