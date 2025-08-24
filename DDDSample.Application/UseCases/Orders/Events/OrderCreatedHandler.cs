using DDDSample.Application.Abstractions;
using DDDSample.Domain.OrderAggregates;
using MediatR;

namespace DDDSample.Application.UseCases.Orders.Events;

public class OrderCreatedHandler : INotificationHandler<DomainEventNotification<OrderCreatedDomainEvent>>
{
    public Task Handle(DomainEventNotification<OrderCreatedDomainEvent> notification, CancellationToken ct)
    {
        // e.g., log, send email, enqueue outbox message, etc.
        Console.WriteLine($"[DomainEvent] Order created: {notification.DomainEvent.OrderId}");
        return Task.CompletedTask;
    }
}
