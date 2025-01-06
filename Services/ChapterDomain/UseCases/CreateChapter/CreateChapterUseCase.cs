using backend.Data.Repository;
using backend.Entities;
using backend.Entities.Dto;
using backend.Services.ChapterDomain.UseCases.CreateChapter;
using backend.Services.NovelDomain.UseCases.GetNovelById;
using backend.Services.ValidationService;

namespace backend.Services.ChapterDomain.UseCases;

public class CreateChapterUseCase:ICreateChapterUseCase
{
    private readonly IChapterRepository _chapterRepository;
    private readonly IChapterValidationService _validationService;
    private readonly IGetNovelUseCase _getNovelUseCase;

    public CreateChapterUseCase(IChapterRepository chapterRepository, IChapterValidationService validationService, IGetNovelUseCase getNovelUseCase)
    {
        _chapterRepository = chapterRepository;
        _validationService = validationService;
        _getNovelUseCase = getNovelUseCase;

    }
    public async Task<Chapter> Execute(CreateChapterDto createChapterDto)
    {
        _validationService.ValidateCreate(createChapterDto);
        
        Novel? novel = null;
        if (Guid.TryParse(createChapterDto.NovelId.ToString(), out Guid novelId))
        {
            novel = await _getNovelUseCase.Execute(novelId);
        }
        else
        {
            novel = await _getNovelUseCase.Execute(createChapterDto.NovelSlug);
        }
        


        if (novel is null)
        {
            throw new Exception("Novel não encontrada2");
        }

        var chapter = new Chapter(
            createChapterDto.Title,
            createChapterDto.Number,
            createChapterDto.Volume,
            createChapterDto.Content
            );
        
        novel.Chapters.Add(chapter);
        await _chapterRepository.Add(chapter);
        
        return chapter;
    }


}