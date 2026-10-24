using CatchUpPlatform.API.News.Domain.Model.ValueObjects;

namespace CatchUpPlatform.API.News.Domain.Model.Commands;

public class CreateFavoriteSourceCommand(NewsApiKey NewsApiKey, SourceId SourceId)
{
    public SourceId SourceId { get; set; }
    public NewsApiKey NewsApiKey { get; set; }
}