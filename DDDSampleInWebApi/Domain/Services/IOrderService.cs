using DDDSampleInWebApi.Domain.Models;

namespace DDDSampleInWebApi.Domain.Services;

/// <summary>
/// The Service Layer contains the business logic that doesn't naturally belong to any single domain 
/// entity but is a part of the application's use cases. 
/// The service acts as an intermediary between the application and the domain layer, 
/// orchestrating operations across multiple entities.
/// </summary>
public interface IOrderService
{
    Task<Order> CreateOrderAsync(Guid customerId);     // Create a new order for a customer
    Task AddLineAsync(Guid orderId, Guid productId, int quantity, decimal unitPrice); // Add a line to an order
    Task RemoveLineAsync(Guid orderId, Guid productId); // Remove a line from an order
    Task CheckoutAsync(Guid orderId);                    // Checkout the order and process it
}
