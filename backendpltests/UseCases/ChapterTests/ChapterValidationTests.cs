using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Validators;
using Moq;

namespace backend_pl_tests.UseCases.ChapterTests;

public class ChapterValidationTests
{
    private readonly ValidationBehavior<UpdateChapterDto> _validationUpdateChapterBehavior;
    private readonly ValidationBehavior<CreateChapterDto> _validationCreateChapterBehavior;
    private readonly Mock<IErrorService> _mockErrorService;
    private readonly CreateChapterValidator _createChapterValidator;
    private readonly UpdateChapterValidator _updateChapterValidator;

    public ChapterValidationTests()
    {
        _mockErrorService = new Mock<IErrorService>();
        _createChapterValidator = new CreateChapterValidator();
        _updateChapterValidator = new UpdateChapterValidator();
        _validationUpdateChapterBehavior =
            new ValidationBehavior<UpdateChapterDto>(_updateChapterValidator, _mockErrorService.Object);
        _validationCreateChapterBehavior =
            new ValidationBehavior<CreateChapterDto>(_createChapterValidator, _mockErrorService.Object);
    }

    [Theory]
    [InlineData("", "Conteúdo válido", 1, 1, "novel-slug")]
    [InlineData("Título válido", "", 1, 1, "novel-slug")]
    [InlineData("Título válido", "Conteúdo válido", -1, 1, "novel-slug")]
    [InlineData("Título válido", "Conteúdo válido", 1, 0, "novel-slug")]
    [InlineData("Título válido", "Conteúdo válido", 1, 1, "")]
    public void ValidateCreate_ShouldThrowException_WhenInvalidInput(string title, string content, int number,
        int volume, string novelSlug)
    {
        var invalidChapter = new CreateChapterDto
        {
            Title = title,
            Content = content,
            Number = number,
            Volume = volume,
            NovelSlug = novelSlug
        };

        var exception =
            Assert.Throws<ErrorCustomException>(() => _validationCreateChapterBehavior.Validate(invalidChapter));
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidateCreate_ShouldPass_ForValidInput()
    {
        var validChapter = new CreateChapterDto
        {
            Title = "Título válido",
            Content = "Conteúdo válido",
            Number = 1,
            Volume = 1,
            NovelSlug = "slug-novela-válido"
        };

        _validationCreateChapterBehavior.Validate(validChapter);
    }

    [Theory]
    [InlineData(null, null, null, null)]
    [InlineData("", "20- caracteres", null, null)]
    [InlineData(
        "abcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabedeabcdeabede",
        null, null, null)]
    public void ValidateUpdate_ShouldThrowException_WhenInvalidInput(string title, string content, int? number,
        int? volume)
    {
        var invalidChapter = new UpdateChapterDto
        {
            Title = title,
            Content = content,
            Number = number,
            Volume = volume
        };

        var exception =
            Assert.Throws<ErrorCustomException>(() => _validationUpdateChapterBehavior.Validate(invalidChapter));
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidateUpdate_ShouldPass_ForValidInput()
    {
        var validChapter = new UpdateChapterDto
        {
            Title = "Título atualizado",
            Content = "Conteúdo atualizado com mais de 20 caracteres",
            Number = 2,
            Volume = 1
        };

        _validationUpdateChapterBehavior.Validate(validChapter);
    }
}