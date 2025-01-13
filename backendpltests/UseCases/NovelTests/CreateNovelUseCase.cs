using backend.Data.Enums;
using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class CreateNovelUseCaseTests : NovelBaseTest
{
    private readonly CreateNovelUseCase _createNovelUseCase;

    public CreateNovelUseCaseTests() =>
        _createNovelUseCase = new CreateNovelUseCase(
            _novelRepository.Object,
            _validationBehavior.Object
        );


    [Fact]
    public async Task Execute_ShouldThrowException_WhenAddToRepositoryFails()
    {
        var createNovelDto = new CreateNovelDto
        {
            Title = "titulo valido",
            Description = "desc valida",
            OriginalLanguage = NovelOriginalLanguage.English
        };

        var newNovel = new Novel(
            createNovelDto.Title,
            createNovelDto.Description,
            createNovelDto.OriginalLanguage,
            createNovelDto.ImageUrl
        );

        _novelRepository
            .Setup(r => r.Add(It.IsAny<Novel>()))
            .ThrowsAsync(new Exception("Failed to add novel"));


        var exception = await Assert.ThrowsAsync<Exception>(() => _novelRepository.Object.Add(newNovel));

        Assert.Equal("Failed to add novel", exception.Message);
    }

    [Fact]
    public async Task Execute_ShouldReturnNewNovel_WhenValidInput()
    {
        var createNovelDto = new CreateNovelDto
        {
            Title = "titulo valido",
            Description = "desc valida",
            OriginalLanguage = NovelOriginalLanguage.English
        };

        _novelRepository
            .Setup(r => r.Add(It.IsAny<Novel>()))
            .Returns(Task.CompletedTask);

        var result = await _createNovelUseCase.Execute(createNovelDto);

        Assert.NotNull(result);
        Assert.Equal("titulo valido", result.Title);
        Assert.Equal("desc valida", result.Description);
        Assert.Equal(NovelOriginalLanguage.English, result.OriginalLanguage);
        Assert.Equal("default.jpg", result.ImageUrl);

        _validationBehavior
            .Verify(
                v => v.Validate(It.IsAny<CreateNovelDto>())
                , Times.Once
            );
        _novelRepository.Verify(r => r.Add(It.IsAny<Novel>()), Times.Once);
    }
}