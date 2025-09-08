using DDDSample.Domain.Abstractions;
using DDDSample.Domain.Repositories;
using MediatR;

namespace DDDSample.Application.UseCases.Orders.Commands.AddItem;

public record AddItemCommand(Guid OrderId, string Sku, int Quantity, decimal UnitPrice) : IRequest<Guid>;


public class AddItemHandler : IRequestHandler<AddItemCommand,Guid>
{
    private readonly IOrderRepository _repo;
    public AddItemHandler(IOrderRepository repo) => _repo = repo;

    public async Task<Guid> Handle(AddItemCommand request, CancellationToken ct)
    {
        try
        {
            var order = await _repo.GetAsync(request.OrderId, ct) ?? throw new KeyNotFoundException("Order not found");
            order.AddItem(request.Sku, request.Quantity, request.UnitPrice);
            await _repo.UnitOfWork.SaveChangesAsync(ct);
            return order.Id;
        }
        catch (DomainException ex)
        {
            return new Guid();
        }
    }

}
