using backend.Data.Repository;
using backend.Entities.Dto;
using backend.Services.ErrorService;
using backend.Utils;
using backendpl.Services.ValidationService;
using Moq;

namespace backendtests
{
    public abstract class TestBase
    {
        protected Mock<IUserRepository> MockUserRepository { get; } = new();
        protected Mock<IErrorService> MockErrorService { get; } = new();
        protected Mock<ITokenGenerator> MockTokenGenerator { get; } = new();
        protected Mock<IValidationBehavior<CreateUserDto>> MockValidationService { get; } = new();
    }
}