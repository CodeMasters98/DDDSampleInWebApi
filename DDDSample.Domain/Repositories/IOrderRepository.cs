using DDDSample.Domain.Abstractions;
using DDDSample.Domain.OrderAggregates;

namespace DDDSample.Domain.Repositories;

public interface IOrderRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AddAsync(Order order, CancellationToken ct = default);
    Task<Order?> GetAsync(Guid id, CancellationToken ct = default);
}
