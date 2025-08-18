using DDDSampleInWebApi.Domain.Common;

namespace DDDSampleInWebApi.Domain.Models
{
    public class OrderLine : Entity
    {
        public ProductId ProductId { get; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; }

        public Money LineTotal => UnitPrice.Multiply(Quantity);

        public OrderLine(ProductId productId, int quantity, Money unitPrice)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            Quantity = quantity;
            UnitPrice = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice));
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            Quantity += quantity;
        }
    }
}
