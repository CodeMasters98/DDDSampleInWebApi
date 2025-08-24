using DDDSample.Domain.Abstractions;

namespace DDDSample.Domain.OrderAggregates;

public record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
