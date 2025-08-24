using DDDSample.Domain.Abstractions;
using DDDSample.Domain.OrderAggregates;
using DDDSample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.InfraStructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Persistence.AppDbContext _db;
    public OrderRepository(Persistence.AppDbContext db) => _db = db;

    public IUnitOfWork UnitOfWork => _db;

    public async Task AddAsync(Order order, CancellationToken ct = default)
    {
        await _db.Orders.AddAsync(order, ct);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Order?> GetAsync(Guid id, CancellationToken ct = default)
    {
        return _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}
