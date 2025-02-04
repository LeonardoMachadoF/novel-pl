using backend.Routes;

namespace backendpl.Extensions;

public static class RouteExtensions
{
    public static void MapApplicationRoutes(this WebApplication app)
    {
        app.MapGroup("/auth").AuthRoutes().WithTags("Auth");
        app.MapGroup("/novel").NovelRoutes().WithTags("Novel");
        app.MapGroup("/{slug}/chapter").ChapterRoutes().WithTags("Chapter");
    }
}
