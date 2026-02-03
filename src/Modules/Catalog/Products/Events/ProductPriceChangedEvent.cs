using Catalog.Products.Models;
using SharedKernel.DDD;

namespace Catalog.Products.Events;

public record ProductPriceChangedEvent(Product Product): IDomainEvent; //record 3shan immutable w bta3 