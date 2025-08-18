using DDDSampleInWebApi.Domain.Models;
using DDDSampleInWebApi.Domain.Repositories;

namespace DDDSampleInWebApi.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Guid customerId)
        {
            // In real applications, you may want to fetch customer details, set other values, etc.
            var order = new Order(Guid.NewGuid()); // Create a new order
            await _orderRepository.SaveAsync(order); // Save the new order
            return order;
        }

        public async Task AddLineAsync(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            // Add line to the order
            order.AddLine(new ProductId(productId), quantity, Money.Of(unitPrice, "USD"));

            await _orderRepository.SaveAsync(order); // Save changes to the order
        }

        public async Task RemoveLineAsync(Guid orderId, Guid productId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            // Remove line from the order
            order.RemoveLine(new ProductId(productId));

            await _orderRepository.SaveAsync(order); // Save changes to the order
        }

        public async Task CheckoutAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            // Perform checkout (this could trigger external services like payment, inventory, etc.)
            order.Checkout();

            await _orderRepository.SaveAsync(order); // Save changes after checkout
        }
    }
}
