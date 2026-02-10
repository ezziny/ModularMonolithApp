using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel.DDD;

namespace SharedKernel.Data.Interceptors;

public class AuditTimesEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void UpdateEntity(DbContext? context)
    {
        if (context is null) return;
        foreach (var entry in context!.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "Admin";
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added || 
                entry.State == EntityState.Modified ||
                entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "Admin";
                entry.Entity.LastModifiedAt = DateTime.UtcNow;
            }
        }
    }
}