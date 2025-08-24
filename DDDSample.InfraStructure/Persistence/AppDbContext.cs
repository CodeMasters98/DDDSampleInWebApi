using System.Reflection;
using DDDSample.Application.Abstractions;
using DDDSample.Domain.Abstractions;
using DDDSample.Domain.OrderAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.InfraStructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options) => _mediator = mediator;


    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }


    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        // Gather domain events from tracked entities BEFORE saving
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .SelectMany(e => e.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(ct);

        // Dispatch after save (or use outbox pattern for reliability)
        foreach (var domainEvent in domainEvents)
        {
            var notification = CreateNotification(domainEvent);
            if (notification is not null)
            {
                await _mediator.Publish(notification, ct);
            }
        }

        // Clear events
        foreach (var entity in ChangeTracker.Entries<Entity>())
        {
            entity.Entity.ClearDomainEvents();
        }


        return result;
    }


    private static INotification? CreateNotification(IDomainEvent domainEvent)
    {
        var domainEventType = domainEvent.GetType();
        var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
        return (INotification?)Activator.CreateInstance(notificationType, domainEvent);
    }
}
