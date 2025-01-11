using backend.Data.Repository;

namespace backend.Services.NovelService.AddToFavorite;

public class AddToFavoriteUseCase: IAddToFavoriteUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly INovelRepository _novelRepository;

    public AddToFavoriteUseCase(IUserRepository userRepository, INovelRepository novelRepository)
    {
        _userRepository = userRepository;
        _novelRepository = novelRepository;
    }
    public async Task Execute(string username, string slug)
    {
        var novel = await _novelRepository.GetNovelBySlug(slug);
        var user = await _userRepository.FindUserInfo(username);
        
        if (!user.Novels.Any(n => n.Slug == novel.Slug))
        {
            await _userRepository.AddFavorite(user, novel);
        }
    }
}