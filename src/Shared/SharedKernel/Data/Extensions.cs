using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Data;

public static class Extensions
{
    public static IApplicationBuilder MigrateAndSeed<TContext>(this IApplicationBuilder builder) where TContext : DbContext
    {
        MigrateDBAsync<TContext>(builder.ApplicationServices).GetAwaiter().GetResult(); //awaiter makes it awaitable and GetResult so you don't have to say Task.Result
        SeedDBAsync<TContext>(builder.ApplicationServices).GetAwaiter().GetResult(); //awaiter makes it awaitable and GetResult so you don't have to say Task.Result
        return builder;
    }

    private static async Task SeedDBAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();  //GET SERVICES OF ALL DATASEEDERS DON'T USE GETSERVICE, USE GETSERVICES 
        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }

    private static async Task MigrateDBAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<TContext>().Database.MigrateAsync();
    }
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
    entry.References.Any(e =>
        e.TargetEntry != null &&
        e.TargetEntry.Metadata.IsOwned() &&
        (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));

}
