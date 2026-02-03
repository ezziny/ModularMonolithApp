using System;
using Catalog.Products.Models;
using SharedKernel.DDD;

namespace Catalog.Products.Events;

public record ProductCreatedEvent(Product Product) : IDomainEvent;
