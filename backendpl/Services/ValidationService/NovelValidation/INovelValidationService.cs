using backend.Entities.Dto;
using backend.Validators;

namespace backend.Services.ValidationService;

public interface INovelValidationService
{
     void ValidateCreate(CreateNovelDto createCreateNovelDto);
     void ValidadeUpdate(UpdateNovelDto updateNovelDto);
     
     void ValidadePagination(IPagination pagination);
}