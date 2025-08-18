namespace DDDSampleInWebApi.Domain.Models
{
    public enum OrderStatus
    {
        Draft,     // Order has been created but not yet placed
        Placed,    // Order has been placed and is being processed
        Paid,      // Order has been paid for
        Shipped,   // Order has been shipped to the customer
        Cancelled  // Order has been cancelled
    }
}
