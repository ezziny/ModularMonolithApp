using System;
using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
{
    private readonly ILogger<ProductPriceChangedEventHandler> _logger;

    public ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Event} handled", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
