using DDDSampleInWebApi.Domain.Models;
using DDDSampleInWebApi.Domain.Repositories;
using DDDSampleInWebApi.Infrastructure.Contexts;

namespace DDDSampleInWebApi.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                                   .Include(o => o.Lines)
                                   .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveAsync(Order order)
        {
            if (await _dbContext.Orders.AnyAsync(o => o.Id == order.Id))
            {
                _dbContext.Orders.Update(order);
            }
            else
            {
                await _dbContext.Orders.AddAsync(order);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Orders.AnyAsync(o => o.Id == id);
        }
    }
}
