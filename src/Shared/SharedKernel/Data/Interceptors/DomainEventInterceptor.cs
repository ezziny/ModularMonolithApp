using System;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel.DDD;

namespace SharedKernel.Data.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DomainEventInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Dispatch(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }



    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await Dispatch(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task Dispatch(DbContext? context)
    {
        if (context is null) return;
        var aggregates = context.ChangeTracker.Entries<IAggregate>()
        .Where(agg => agg.Entity.DomainEvents.Any())
        .Select(agg => agg.Entity);

        var domainEvents = aggregates.SelectMany(agg => agg.DomainEvents).ToList();
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
        aggregates.ToList().ForEach(agg => agg.ClearDomainEvents());

    }
}
