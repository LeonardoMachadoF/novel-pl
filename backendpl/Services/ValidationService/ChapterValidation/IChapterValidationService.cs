using backend.Entities.Dto;

namespace backend.Services.ValidationService;

public interface IChapterValidationService
{
    void ValidateCreate(CreateChapterDto createChapterDto);
    void ValidateUpdate(UpdateChapterDto updateChapterDto);
}