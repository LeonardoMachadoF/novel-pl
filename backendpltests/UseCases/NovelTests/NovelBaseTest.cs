using backend.Data.Repository;
using backend.Entities.Dto;
using backend.Services.NovelServices.UseCases.CreateNovel;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public abstract class NovelBaseTest
{
    protected readonly Mock<INovelRepository> _novelRepository = new();
    protected readonly Mock<IValidationBehavior<CreateNovelDto>> _validationBehavior = new();
}