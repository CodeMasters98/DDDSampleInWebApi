using DDDSample.Application.Abstractions;
using DDDSample.Domain.OrderAggregates.Events;
using MediatR;

namespace DDDSample.Application.UseCases.Orders.Events;

public class OrderPlacedHandler : INotificationHandler<DomainEventNotification<OrderPlacedDomainEvent>>
{
    public Task Handle(DomainEventNotification<OrderPlacedDomainEvent> notification, CancellationToken ct)
    {
        Console.WriteLine($"[DomainEvent] Order placed: {notification.DomainEvent.OrderId} Total={notification.DomainEvent.TotalAmount}");
        return Task.CompletedTask;
    }
}
