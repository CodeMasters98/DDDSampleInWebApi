using DDDSampleInWebApi.Domain.Common;
using DDDSampleInWebApi.Domain.Models;

namespace DDDSampleInWebApi.Domain.Events
{
    public class OrderPlaced : IDomainEvent
    {
        public Guid OrderId { get; }
        public Money TotalAmount { get; }
        public DateTime OccurredOn { get; }

        public OrderPlaced(Guid orderId, Money totalAmount)
        {
            //Validate
            OrderId = orderId;
            TotalAmount = totalAmount;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
