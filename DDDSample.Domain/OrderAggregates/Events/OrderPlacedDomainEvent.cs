using DDDSample.Domain.Abstractions;

namespace DDDSample.Domain.OrderAggregates.Events;

public record OrderPlacedDomainEvent(Guid OrderId, decimal TotalAmount) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
