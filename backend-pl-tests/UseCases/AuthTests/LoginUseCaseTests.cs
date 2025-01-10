using backend.Data.Repository;
using backend.Entities;
using backend.Services.AuthDomain;
using backend.Utils;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace backendtests.UseCases;

public class LoginUseCaseTests: TestBase
{
    private readonly PasswordHasher<User> _passwordHasher = new();
    
    private User CreateUser(string username, string password) => new()
    {
        Username = username,
        Password = _passwordHasher.HashPassword(null, password)
    };

    [Fact]
    public async Task Execute_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        const string username = "validusername";
        const string password = "validpassword";

        MockUserRepository
            .Setup(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username))!
            .ReturnsAsync((User)null!);

        var useCase = new LoginUseCase(MockUserRepository.Object, MockTokenGenerator.Object);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Execute(username, password));

        // Assert
        Assert.Equal("User not found", exception.Message);
        MockUserRepository.Verify(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenPasswordVerificationFails()
    {
        const string username = "validusername";
        const string invalidPassword = "invalidpassword";
        const string existentUserPassword = "validpassword";
        var existentUser = CreateUser(username, existentUserPassword);

        MockUserRepository
            .Setup(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username))
            .ReturnsAsync(existentUser);
        var useCase = new LoginUseCase(MockUserRepository.Object, MockTokenGenerator.Object);
        
        var result = await useCase.Execute(username, invalidPassword);
        
        Assert.Null(result);
        MockUserRepository.Verify(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnToken_WhenPasswordVerificationSucceeds()
    {
        const string username = "validusername";
        const string password = "validpassword";
        const string expectedToken = "generated_token";
        
        var user = CreateUser(username, password);
        MockUserRepository
            .Setup(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username))
            .ReturnsAsync(user);
        MockTokenGenerator
            .Setup(gen => gen.GenerateToken(user))
            .Returns(expectedToken);
        
        var useCase = new LoginUseCase(MockUserRepository.Object, MockTokenGenerator.Object);
        
        var result = await useCase.Execute(username, password);
        
        Assert.NotNull(result);
        Assert.Equal(expectedToken, result);
        MockUserRepository.Verify(repo => repo.FindUserByEmailOrUsermail(It.IsAny<string>(), username), Times.Once);
        MockTokenGenerator.Verify(gen => gen.GenerateToken(user), Times.Once);
    }
}