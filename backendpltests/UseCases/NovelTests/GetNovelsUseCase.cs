using backend.Data.Enums;
using backend.Data.Repository;
using backend.Entities;
using backend.Services.NovelServices.UseCases.GetNovels;
using backend.Validators;
using backendpl.Services.ValidationService;
using Moq;

namespace backend_pl_tests.UseCases.AuthNovel;

public class GetNovelsUseCaseTests: NovelBaseTest
{
    private readonly Mock<IValidationBehavior<IPagination>> _mockValidator = new();
    private readonly GetNovelsUseCase _getNovelsUseCase;
    public GetNovelsUseCaseTests()
    {
        _getNovelsUseCase = new GetNovelsUseCase(_novelRepository.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetNovels_ShouldReturnNovels_WhenCalledWithValidParameters()
    {
        var expectedNovels = new List<Novel>
        {
            new Novel("valid title", "valid description", NovelOriginalLanguage.English, "default.jpg"),
            new Novel("valid title2", "valid description2", NovelOriginalLanguage.English, "default.jpg")
        };

        _novelRepository
            .Setup(m => m.GetNovels(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expectedNovels);


        var result = await _getNovelsUseCase.Execute(2, 0);
        
        Assert.NotNull(result);
        Assert.IsType<List<Novel>>(result); 
        Assert.Equal(expectedNovels.Count, result.Count); 

        for (int i = 0; i < expectedNovels.Count; i++)
        {
            Assert.Equal(expectedNovels[i].Title, result[i].Title);
            Assert.Equal(expectedNovels[i].Description, result[i].Description);
            Assert.Equal(expectedNovels[i].OriginalLanguage, result[i].OriginalLanguage);
            Assert.Equal(expectedNovels[i].ImageUrl, result[i].ImageUrl);
        }
    }
}