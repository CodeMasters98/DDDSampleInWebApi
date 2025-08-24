using DDDSampleInWebApi.Domain.Common;

namespace DDDSampleInWebApi.Domain.Events
{
    public class OrderHistory : IDomainEvent
    {
        public Guid OrderId { get; }
        public DateTime OccurredOn { get; }

        public OrderHistory(Guid OrderId)
        {

        }
    }
}
