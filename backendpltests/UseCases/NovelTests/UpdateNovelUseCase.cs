using backend.Data.Enums;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.NovelServices.UseCases.UpdateNovel;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class UpdateNovelUseCaseTests : NovelBaseTest
{
    private readonly Mock<IValidationBehavior<UpdateNovelDto>> _userRepositoryMock = new();
    private readonly IUpdateNovelUseCase _updateNovelUseCase;

    public UpdateNovelUseCaseTests() => _updateNovelUseCase =
        new UpdateNovelUseCase(_novelRepository.Object, _userRepositoryMock.Object);


    [Fact]
    public async Task Execute_ShouldThrowException_WhenNovelNotFound()
    {
        var slug = "valid slug";
        var novelDto = new UpdateNovelDto
        {
            Title = "titulo valido",
            Description = "Descrição valida",
            OriginalLanguage = NovelOriginalLanguage.English
        };

        _novelRepository
            .Setup(x => x.GetNovelBySlug(slug))
            .ReturnsAsync((Novel)null!);

        var exception = await Assert.ThrowsAsync<Exception>(() => _updateNovelUseCase.Execute(slug, novelDto));

        Assert.NotNull(exception);
        Assert.Equal("Novel não encontrada", exception.Message);
    }

    [Theory]
    [InlineData("titulo valido", null, null)]
    [InlineData(null, "Descrição válida", null)]
    [InlineData(null, null, NovelOriginalLanguage.English)]
    [InlineData("titulo valido", "Descrição válida", null)]
    [InlineData("titulo valido", null, NovelOriginalLanguage.English)]
    [InlineData(null, "Descrição válida", NovelOriginalLanguage.English)]
    public async Task Execute_ShouldReturnNovelUpdated_WhenValidInput(string title, string description,
        NovelOriginalLanguage? originalLanguage)
    {
        var novelDto = new UpdateNovelDto
        {
            Title = title,
            Description = description,
            OriginalLanguage = originalLanguage
        };

        var novel = new Novel(
            "slug-exemplo",
            "Titulo antigo",
            NovelOriginalLanguage.Portuguese,
            "image.jpg"
        );

        _novelRepository
            .Setup(x => x.GetNovelBySlug(novel.Slug))
            .ReturnsAsync(novel);

        var result = await _updateNovelUseCase.Execute(novel.Slug, novelDto);

        Assert.NotNull(result);
        Assert.Equal(title ?? novel.Title, result.Title);
        Assert.Equal(description ?? novel.Description, result.Description);
        Assert.Equal(originalLanguage ?? novel.OriginalLanguage, result.OriginalLanguage);
    }
}