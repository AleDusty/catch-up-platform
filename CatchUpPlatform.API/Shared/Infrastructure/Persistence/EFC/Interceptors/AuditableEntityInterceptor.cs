
using CatchUpPlatform.API.Shared.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Interceptors;

public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditTimestamp(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyAuditTimestamp(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private static void ApplyAuditTimestamp(DbContext? context)
    {
        if(context is null )return;
        var now= DateTimeOffset.UtcNow;
        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if(entry.State is EntityState.Added or EntityState.Modified) entry.Entity.UpdatedAt = now;
            if(entry.State is EntityState.Added) entry.Entity.CreatedAt = now;
        }
    }
}