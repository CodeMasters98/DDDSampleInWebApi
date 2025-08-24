using DDDSample.Domain.Abstractions;
using MediatR;

namespace DDDSample.Application.Abstractions;

public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification where TDomainEvent : IDomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}
