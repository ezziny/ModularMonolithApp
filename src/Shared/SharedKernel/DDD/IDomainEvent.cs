using MediatR;
using Microsoft.AspNetCore.Components.Web;

namespace SharedKernel.DDD;

public interface IDomainEvent : INotification
{
    private Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}
