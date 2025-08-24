using DDDSampleInWebApi.Domain.Common;
using DDDSampleInWebApi.Domain.Events;

namespace DDDSampleInWebApi.Domain.Models
{
    public class Order : Entity, IAggregateRoot
    {
        private readonly List<OrderLine> _lines = new();
        public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();
        public OrderStatus Status { get; private set; }

        // Constructor for creating a new order
        public Order(Guid id)
        {
            Id = id;
            Status = OrderStatus.Draft;
        }

        // Business method to add a line to the order
        public void AddLine(ProductId productId, int qty, Money unitPrice)
        {
            if (Status != OrderStatus.Draft)
                throw new InvalidOperationException("Cannot modify a non-draft order.");

            var existingLine = _lines.Find(line => line.ProductId == productId);
            if (existingLine != null)
                existingLine.IncreaseQuantity(qty);
            else
                _lines.Add(new OrderLine(productId, qty, unitPrice));
        }

        public void Pendding()
        {
            //Logic Validate
            if (Status == OrderStatus.Shipped)
                throw new InvalidOperationException("Cannot checkout a non-draft order.");

            Status = OrderStatus.Placed;
            Raise(new OrderPlaced(Id, CalculateTotal()));
        }

        // Business method to checkout the order
        public void Checkout()
        {
            if (Status != OrderStatus.Draft)
                throw new InvalidOperationException("Cannot checkout a non-draft order.");

            if (_lines.Count == 0)
                throw new InvalidOperationException("Cannot checkout an empty order.");

            Status = OrderStatus.Placed;

            // Raise the domain event
            Raise(new OrderPlaced(Id, CalculateTotal()));
        }

        // Method to calculate the total of the order
        private Money CalculateTotal() => _lines.Aggregate(Money.Zero("USD"), (sum, line) => sum.Add(line.LineTotal));
    }
}
