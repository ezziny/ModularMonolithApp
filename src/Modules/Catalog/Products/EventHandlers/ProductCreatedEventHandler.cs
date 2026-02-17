using System;
using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Event} handled", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
