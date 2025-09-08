using DDDSample.Domain.Abstractions;
using DDDSample.Domain.OrderAggregates.Exceptions;
namespace DDDSample.Domain.OrderAggregates;

public class Order : Entity
{
    private readonly List<OrderItem> _items = new();


    // EF Core requires a private/protected ctor
    private Order() { }

    public Order(Guid id, string customerName)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        Status = OrderStatus.Created;
        Raise(new OrderCreatedDomainEvent(Id));
    }


    public Guid Id { get; private set; }
    public string CustomerName { get; private set; } = default!;
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;


    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();


    public void AddItem(string sku, int quantity, decimal unitPrice)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOrderStateException(new Guid());

        var item = new OrderItem(Guid.NewGuid(), Id, sku, quantity, unitPrice);
        _items.Add(item);


        Raise(new Events.OrderItemAddedDomainEvent(Id, item.Id, sku, quantity, unitPrice));
    }


    public decimal Total() => _items.Sum(i => i.Quantity * i.UnitPrice);


    public void Place()
    {
        if (!_items.Any()) throw new InvalidOperationException("Order must have at least one item.");
        if (Status != OrderStatus.Created) throw new InvalidOperationException("Order already placed.");
        Status = OrderStatus.Placed;
        Raise(new Events.OrderPlacedDomainEvent(Id, Total()));
    }
}
