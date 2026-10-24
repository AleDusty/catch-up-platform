namespace CatchUpPlatform.API.News.Application.Errors;

public enum CreateFavoriteSourceError
{
    InvalidNewsApiKey,
    InvalidSourceId,
    DuplicateFavoriteSource,
    UnexpectedError
}