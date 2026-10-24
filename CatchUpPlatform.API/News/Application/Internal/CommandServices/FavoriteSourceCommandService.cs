using CatchUpPlatform.API.News.Application.Errors;
using CatchUpPlatform.API.News.Application.Services;
using CatchUpPlatform.API.News.Domain.Model.Aggregates;
using CatchUpPlatform.API.News.Domain.Model.Commands;
using CatchUpPlatform.API.News.Domain.Repository;
using CatchUpPlatform.API.Shared.Applications.Patterns;
using CatchUpPlatform.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatchUpPlatform.API.News.Application.Internal.CommandServices;

public class FavoriteSourceCommandService(
    IFavoriteSourceRepository favoriteSourceRepository,
    IUnitOfWork unitOfWork,
    ILogger<FavoriteSourceCommandService> logger)
:IFavoriteSourceCommandService
{
    public async Task<Result<FavoriteSource, CreateFavoriteSourceError>> Handle(CreateFavoriteSourceCommand command, 
        CancellationToken cancellationToken = default)
    
    {
        var existingSource = await favoriteSourceRepository.FindByNewsApiKeyAndSourceIdAsync(command.NewsApiKey,
            command.SourceId, cancellationToken);
        if (existingSource != null)
        {
            logger.LogWarning(
                "Duplicate favorite source rejeted for newsApiKey",command.NewsApiKey, command.SourceId);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Failure
                (CreateFavoriteSourceError.DuplicateFavoriteSource);
        }

        try
        {
            var favoriteSource = new FavoriteSource(command.NewsApiKey, command.SourceId);
            await favoriteSourceRepository.AddAsync(favoriteSource, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Success(favoriteSource);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, 
                "Invalid arguments while creating Favorite source for NewsApiKey {NewsApiKey} and SourceId {SourceId}",
                command.NewsApiKey, 
                command.SourceId);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Failure(
                CreateFavoriteSourceError.UnexpectedError);
        }
        catch (DbUpdateException ex) when (IsDuplicateKeyViolation(ex))
        {
            logger.LogWarning(ex, 
                "Duplicate key violarion creating source for NewsApiKey {NewsApiKey} and SourceId {SourceId}",
                command.NewsApiKey, 
                command.SourceId);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Failure(
                CreateFavoriteSourceError.DuplicateFavoriteSource);
        }
        catch (DbUpdateException ex) 
        {
            logger.LogWarning(ex, 
                "Database update failed creating source for NewsApiKey {NewsApiKey} and SourceId {SourceId}",
                command.NewsApiKey, 
                command.SourceId);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Failure(
                CreateFavoriteSourceError.UnexpectedError);
        }
        catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
        {
            throw;

        }
        catch (Exception ex) 
        {
            logger.LogWarning(ex, 
                "Unexpected error creating favorite source for NewsApiKey {NewsApiKey} and SourceId {SourceId}",
                command.NewsApiKey, 
                command.SourceId);
            return new Result<FavoriteSource, CreateFavoriteSourceError>.Failure(
                CreateFavoriteSourceError.UnexpectedError);
        }
    }

    private static bool IsDuplicateKeyViolation(DbUpdateException exception)
    {
        for (Exception? current = exception; current != null; current = current.InnerException)
        {
            if (!String.Equals(current.GetType().Name, "MySqlException", StringComparison.Ordinal)) continue;
            var numberProperty = current.GetType().GetProperty("Number");
            if(numberProperty?.PropertyType==typeof(int)&&
                numberProperty.GetValue(current) is int errorCode &&
                errorCode==1062)
                return true;
        }

        return false;
    }
}