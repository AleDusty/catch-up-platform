using CatchUpPlatform.API.News.Domain.Model.ValueObjects;

namespace CatchUpPlatform.API.News.Domain.Model.Aggregates;

public partial class FavoriteSource
{
    protected FavoriteSource()
    {
        NewsApiKey = null;
        SourceId = null;
    }
    public int Id { get; set; }
    public NewsApiKey NewsApiKey{ get; private set; }
    public SourceId SourceId { get; private set; }
    
    public FavoriteSource(NewsApiKey newsApiKey, SourceId sourceId)
    {
        NewsApiKey = newsApiKey;
        SourceId = sourceId;
    }
}
