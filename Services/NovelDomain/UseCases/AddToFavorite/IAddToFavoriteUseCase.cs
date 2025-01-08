namespace backend.Services.NovelService.AddToFavorite;

public interface IAddToFavoriteUseCase
{
    Task Execute(string username, string slug);
}