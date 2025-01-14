using backend.Services.ErrorService;
using backend.Validators;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace backend_pl_tests.UseCases.Utils;

public class PaginationValidationTests
{
    private readonly ValidationBehavior<IPagination> _paginationValidator;

    public PaginationValidationTests()
    {
        var validatorMock = new GetPaginationValidator();
        Mock<IErrorService> mockErrorService = new();
        _paginationValidator = new ValidationBehavior<IPagination>(
            validatorMock,
            mockErrorService.Object
        );
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(10, 5)]
    [InlineData(39, 0)]
    public void Validate_ShouldNotThrowValidationException_WhenPaginationIsValid(int take, int skip)
    {
        var validPagination = new Pagination(take, skip);

        var result = Record.Exception(() =>
            _paginationValidator.Validate(validPagination));

        Assert.Null(result);
    }

    [Theory]
    [InlineData(0, -1)]
    [InlineData(-1, 0)]
    [InlineData(40, 0)]
    [InlineData(-10, -5)]
    public void Validate_ShouldThrowValidationException_WhenPaginationIsInvalid(int take, int skip)
    {
        var invalidPagination = new Pagination(take, skip);

        var exception = Assert
            .Throws<ErrorCustomException>(
                () => _paginationValidator.Validate(invalidPagination)
            );
        Assert.NotNull(exception);
    }
}