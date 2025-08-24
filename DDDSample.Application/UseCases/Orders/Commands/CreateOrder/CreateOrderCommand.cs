using DDDSample.Domain.OrderAggregates;
using DDDSample.Domain.Repositories;
using MediatR;

namespace DDDSample.Application.UseCases.Orders.Commands.CreateOrder;

public record CreateOrderCommand(string CustomerName) : IRequest<Guid>;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repo;

    public CreateOrderHandler(IOrderRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var order = new Order(Guid.NewGuid(), request.CustomerName);
        await _repo.AddAsync(order, ct);
        return order.Id;
    }
}
