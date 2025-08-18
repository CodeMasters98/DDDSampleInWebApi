using DDDSampleInWebApi.Domain.Models;

namespace DDDSampleInWebApi.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);             // Retrieve an order by its ID
        Task SaveAsync(Order order);                   // Save or update the order
        Task RemoveAsync(Order order);                 // Remove the order from persistence
        Task<bool> ExistsAsync(Guid id);               // Check if an order exists
    }
}
