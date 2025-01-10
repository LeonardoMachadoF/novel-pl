// using backend.Data.Repository;
// using backend.Entities;
// using backend.Services.NovelServices.UseCases.GetNovelById;
//
//
// namespace backend.Services.NovelServices.UseCases.GetNovelBySlug;
//
// public class GetNovelBySlugUseCase:IGetNovelBySlugUseCase
// {
//     private readonly INovelRepository _novelRepository;
//     
//     public GetNovelBySlugUseCase(INovelRepository novelRepository)
//     {
//         _novelRepository = novelRepository;
//     }
//     
//     public async Task<Novel?> Execute(string slug)
//     {
//         var novel = await _novelRepository.GetNovelBySlug(slug);
//         if (novel == null)
//         {
//             throw new Exception("Novel não encontrada");
//         }
//         return novel;
//     }
// }