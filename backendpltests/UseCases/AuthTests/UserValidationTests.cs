using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Validators;
using Moq;

namespace backendtests.UseCases;

public class UserValidationServiceTests
{
    private readonly ValidationBehavior<CreateUserDto> _validationBehavior;
    private readonly Mock<IErrorService> _mockErrorService;
    private readonly CreateUserValidator _createUserValidator;

    public UserValidationServiceTests()
    {
        _mockErrorService = new Mock<IErrorService>();
        _createUserValidator = new CreateUserValidator();
        _validationBehavior = new ValidationBehavior<CreateUserDto>(_createUserValidator, _mockErrorService.Object);
    }

    [Theory]
    [InlineData("invalid_email", "validUser", "password123")]
    [InlineData("validuser@email.com", "invalid user!", "password123")]
    [InlineData("validuser@email.com", "validUser", "short")]
    public void ValidateCreate_ShouldThrowException_WhenInvalidInput(string email, string username, string password)
    {
        var invalidUser = new CreateUserDto
        {
            Email = email,
            Username = username,
            Password = password
        };

        var exception = Assert.Throws<ErrorCustomException>(() => _validationBehavior.Validate(invalidUser));
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidateCreate_ShouldPass_ForValidInput()
    {
        var validUser = new CreateUserDto
        {
            Email = "validuser@email.com",
            Username = "validUser",
            Password = "password123"
        };

        _validationBehavior.Validate(validUser);
    }
}