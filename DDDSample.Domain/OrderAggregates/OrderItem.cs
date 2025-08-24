namespace DDDSample.Domain.OrderAggregates;

public class OrderItem
{
    private OrderItem() { }
    public OrderItem(Guid id, Guid orderId, string sku, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));


        Id = id;
        OrderId = orderId;
        Sku = sku;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }


    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Sku { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
}
