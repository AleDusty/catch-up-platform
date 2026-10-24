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
    public Task<Result<FavoriteSource, CreateFavoriteSourceError>> Handle(CreateFavoriteSourceCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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