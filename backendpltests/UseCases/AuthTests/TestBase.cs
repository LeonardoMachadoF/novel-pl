using backend.Data.Repository;
using backend.Services.ErrorService;
using backend.Services.ValidationService;
using backend.Utils;
using Moq;

namespace backendtests
{
    public abstract class TestBase
    {
        protected Mock<IUserRepository> MockUserRepository { get; } = new();
        protected Mock<IErrorService> MockErrorService { get; } = new();
        protected Mock<ITokenGenerator> MockTokenGenerator { get; } = new();
        protected Mock<IUserValidationService> MockValidationService { get; } = new();
    }
}