using DDDSample.Domain.Repositories;
using MediatR;

namespace DDDSample.Application.UseCases.Orders.Commands.PlaceOrder;

public record PlaceOrderCommand(Guid OrderId) : IRequest<bool>;

public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand,bool>
{
    private readonly IOrderRepository _repo;
    public PlaceOrderHandler(IOrderRepository repo) => _repo = repo;


    public async Task<bool> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        var order = await _repo.GetAsync(request.OrderId, ct) ?? throw new KeyNotFoundException("Order not found");
        order.Place();
        var result = await _repo.UnitOfWork.SaveChangesAsync(ct);
        return result == 1;
    }
}
