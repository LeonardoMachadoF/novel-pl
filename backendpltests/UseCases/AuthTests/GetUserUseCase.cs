using backend.Data.Repository;
using backend.Entities;
using backend.Services.AuthDomain.GetUser;
using Moq;

namespace backendtests.UseCases;

public class GetUserUseCaseTests: TestBase
{
    [Fact]
    public async Task Execute_ShouldReturnUser_WhenUserExists()
    {
        string username = "daisjdasdas";
        var existingUser = new User
        {
            Email = "aisuydadas@gmail.com",
            Username = "daisjdasdas",
            Password = "dasihjdas"
        };
        MockUserRepository.Setup(repo=>repo.FindUserInfo("daisjdasdas")).ReturnsAsync(existingUser);
        
        var getUserUseCase = new GetUserUseCase(MockUserRepository.Object);
        var result = await getUserUseCase.Execute(username);
        
        Assert.NotNull(result);
        Assert.Equal(existingUser.Username, result.Username);
        Assert.Equal(existingUser.Email, result.Email);
        MockUserRepository.Verify(repo=>repo.FindUserInfo("daisjdasdas"), Times.Once);
    }
    
    [Fact]
    public async Task Execute_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var username = "naoexistente";
        MockUserRepository.Setup(repo => repo.FindUserInfo(username)).ReturnsAsync((User)null);
        
        
        var useCase = new GetUserUseCase(MockUserRepository.Object);
        var result = await useCase.Execute(username);


        Assert.Null(result);
        MockUserRepository.Verify(repo => repo.FindUserInfo(username), Times.Once);
    }
}