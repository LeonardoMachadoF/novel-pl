using backend.Entities;
using backend.Entities.Dto;
using backend.Services.AuthDomain.CreateUser;
using Moq;

namespace backendtests.UseCases;

public class CreateUserUseCaseTests: TestBase
{
    private CreateUserDto CreateValidUserDto(string email, string username, string password)
    {
        return new CreateUserDto
        {
            Email = email,
            Username = username,
            Password = password
        };
    }

    [Fact]
    public async Task Execute_ShouldCreateUser_ForValidInputs()
    {
        var validUserDto = CreateValidUserDto("newuser@example.com", "newuser", "password123");

        MockValidationService.Setup(v => v.Validate(validUserDto));
        MockUserRepository.Setup(repo => repo.FindUserByEmailOrUsermail(validUserDto.Email, validUserDto.Username))
            .ReturnsAsync((User)null);
        MockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).Returns(Task.CompletedTask);

        var useCase = new CreateUserUseCase(MockValidationService.Object, MockUserRepository.Object);

        var result = await useCase.Execute(validUserDto);

        Assert.NotNull(result);
        Assert.Equal(validUserDto.Email, result.Email);
        Assert.Equal(validUserDto.Username, result.Username);
        MockUserRepository.Verify(repo => repo.Add(It.Is<User>(user => user.Email == validUserDto.Email && user.Username == validUserDto.Username)), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var validUserDto = CreateValidUserDto("existinguser@email.com", "newuser", "password123");
        var existingUser = new User { Email = "existinguser@email.com", Username = "existinguser", Password = "hashedPassword" };

        MockValidationService.Setup(v => v.Validate(It.IsAny<CreateUserDto>()));
        MockUserRepository.Setup(repo => repo.FindUserByEmailOrUsermail(validUserDto.Email, validUserDto.Username))
            .ReturnsAsync(existingUser);

        var useCase = new CreateUserUseCase(MockValidationService.Object, MockUserRepository.Object);

        var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Execute(validUserDto));
        Assert.Equal("User already existed", exception.Message);
        MockUserRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Never);
    }
}