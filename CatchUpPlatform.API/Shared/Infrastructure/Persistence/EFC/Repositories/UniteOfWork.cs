using CatchUpPlatform.API.Shared.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
 
public class UniteOfWork(AppDbContext context): IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}