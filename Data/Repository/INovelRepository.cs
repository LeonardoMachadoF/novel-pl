using backend.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Data.Repository;

public interface INovelRepository
{
    Task Add(Novel novel);
    Task<List<Novel>> FindNovels();
}
