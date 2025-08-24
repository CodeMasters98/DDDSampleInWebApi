using DDDSample.Domain.Abstractions;

namespace DDDSample.Domain.OrderAggregates.Events;

public record OrderItemAddedDomainEvent(Guid OrderId, Guid OrderItemId, string Sku, int Quantity, decimal UnitPrice) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}
