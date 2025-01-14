using backend.Data.Enums;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Validators;
using FluentValidation.Results;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class NovelValidationTests
{
    private readonly ValidationBehavior<UpdateNovelDto> _validationUpdateNovelBehavior;
    private readonly ValidationBehavior<CreateNovelDto> _validationCreateNovelBehavior;

    public NovelValidationTests()
    {
        Mock<IErrorService> mockErrorService = new();
        var createNovelValidator = new CreateNovelValidator();
        var updateNovelValidator = new UpdateNovelValidator();

        _validationCreateNovelBehavior = new ValidationBehavior<CreateNovelDto>(
            createNovelValidator,
            mockErrorService.Object
        );

        _validationUpdateNovelBehavior = new ValidationBehavior<UpdateNovelDto>(
            updateNovelValidator,
            mockErrorService.Object
        );
    }

    [Theory]
    [InlineData("", "descrição válida", NovelOriginalLanguage.English)]
    [InlineData("titulo valido", "", NovelOriginalLanguage.English)]
    [InlineData("titulo valido", "descrição válida", (NovelOriginalLanguage)999)]
    public void Validade_ShouldThrowValidationException_WhenCreateNovelIsInvalid(
        string title, string description, NovelOriginalLanguage originalLanguage)
    {
        var invalidNovelDto = new CreateNovelDto
        {
            Title = title,
            Description = description,
            OriginalLanguage = originalLanguage
        };

        var exception = Assert
            .Throws<ErrorCustomException>(
                () => _validationCreateNovelBehavior.Validate(invalidNovelDto)
            );
        Assert.NotNull(exception);
    }

    [Fact]
    public void Validade_ShouldNotThrowValidationException_WhenCreateNovelIsValid()
    {
        var validNovelDto = new CreateNovelDto
        {
            Title = "Valid Novel",
            Description = "This is a valid description.",
            OriginalLanguage = NovelOriginalLanguage.English
        };

        var result = Record.Exception(() =>
            _validationCreateNovelBehavior.Validate(validNovelDto));

        Assert.Null(result);
    }

    [Theory]
    [InlineData("", "", (NovelOriginalLanguage)55)]
    public void Validade_ShouldThrowValidationException_WhenUpdateNovelIsInvalid(
        string title, string description, NovelOriginalLanguage? originalLanguage)
    {
        var invalidNovelDto = new UpdateNovelDto
        {
            Title = title,
            Description = description,
            OriginalLanguage = originalLanguage
        };

        var exception = Assert.Throws<ErrorCustomException>(() =>
            _validationUpdateNovelBehavior.Validate(invalidNovelDto));

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData("hfghfgh", "", null)]
    [InlineData("", "fhghfghghegfhddfghfdghfd", null)]
    [InlineData("", "", NovelOriginalLanguage.English)]
    [InlineData("dasdasdasd", "fhghfghghegfhddfghfdghfd", null)]
    [InlineData("gsdfgsdfgsdfgsdf", "gsdfgsdfgsdf", NovelOriginalLanguage.English)]
    public void Validade_ShouldNotThrowValidationException_WhenUpdateNovelValid(
        string title, string description, NovelOriginalLanguage? originalLanguage)
    {
        var invalidNovelDto = new UpdateNovelDto
        {
            Title = title,
            Description = description,
            OriginalLanguage = originalLanguage
        };

        var exception = Record.Exception(() =>
            _validationUpdateNovelBehavior.Validate(invalidNovelDto));

        Assert.Null(exception);
    }
}