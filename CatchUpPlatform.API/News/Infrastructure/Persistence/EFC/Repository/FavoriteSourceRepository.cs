using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.ValueObjects;
using CatchUpPlatform.API.News.Domain.Repository;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatchUpPlatform.API.News.Infrastructure.Persistence.EFC.Repository;

public class FavoriteSourceRepository(AppDbContext context)
    :BaseRepository<FavoriteSource>(context),IFavoriteSourceRepository
{
    public async Task<IEnumerable<FavoriteSource>> FindByNewsApiKeyAsync(NewsApiKey newsApiKey, CancellationToken cancellationToken = default)
    {
        return await Context.Set<FavoriteSource>()
            .Where(x => x.NewsApiKey == newsApiKey)
            .ToListAsync(cancellationToken);
    }

    public async Task<FavoriteSource?> FindByNewsApiKeyAndSourceIdAsync(NewsApiKey newsApiKey, SourceId sourceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<FavoriteSource>()
            .FirstOrDefaultAsync(s => s.NewsApiKey == newsApiKey && s.SourceId == sourceId, 
                cancellationToken);
    }
}