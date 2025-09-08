using DDDSample.Domain.Abstractions;

namespace DDDSample.Domain.OrderAggregates.Exceptions;

public class InvalidOrderStateException : DomainException
{
    public InvalidOrderStateException(Guid orderId)
       : base($"Order {orderId} cannot be shipped because it is not paid.") { }

    public override string Code => "order.unpaid_ship";
}
