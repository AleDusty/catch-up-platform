using CatchUpPlatform.API.News.Application.Services;
using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Queries;
using CatchUpPlatform.API.News.Domain.Repository;

namespace CatchUpPlatform.API.News.Application.Internal.QueryServices;

public class FavoriteSourceQueryService(
    IFavoriteSourceRepository favoriteSourceRepository) 
    :IFavoriteSourceQueryServices
{
    public async Task<IEnumerable<FavoriteSource>> Handle(GetAllFavoriteSourcesByNewsApiKeyQuery query,
        CancellationToken cancellationToken = default) => 
        await favoriteSourceRepository.FindByNewsApiKeyAndSourceIdAsync(query.NewsApiKey,cancellationToken);

    public async Task<FavoriteSource> Handle(GetFavoriteSourceByIdQuery query, 
        CancellationToken cancellationToken = default) => 
        await favoriteSourceRepository.FindByIdAsync(query.Id,cancellationToken);

    public async Task<FavoriteSource> Handle(GetFavoriteSourceNewsApiKeyAddSourceIdQuery query, 
        CancellationToken cancellationToken = default) => 
        await favoriteSourceRepository.FindByNewsApiKeyAndSourceIdAsync(query.NewsApiKey,query.SourceId,cancellationToken);
}