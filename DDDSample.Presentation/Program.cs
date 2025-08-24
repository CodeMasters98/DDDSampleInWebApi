using DDDSample.Application.UseCases.Orders.Commands.AddItem;
using DDDSample.Application.UseCases.Orders.Commands.CreateOrder;
using DDDSample.Application.UseCases.Orders.Commands.PlaceOrder;
using DDDSample.Domain.Repositories;
using DDDSample.InfraStructure.Persistence;
using DDDSample.InfraStructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("OrdersDb"));

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly));

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orders", async (CreateOrderCommand cmd, IMediator mediator) =>
{
    var id = await mediator.Send(cmd);
    return Results.Created($"/orders/{id}", new { id });
});


app.MapPost("/orders/{id:guid}/items", async (Guid id, AddItemCommand body, IMediator mediator) =>
{
    await mediator.Send(body with { OrderId = id });
    return Results.NoContent();
});


app.MapPost("/orders/{id:guid}/place", async (Guid id, IMediator mediator) =>
{
    await mediator.Send(new PlaceOrderCommand(id));
    return Results.NoContent();
});


app.MapGet("/orders/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var order = await db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
    return order is null ? Results.NotFound() : Results.Ok(new
    {
        order.Id,
        order.CustomerName,
        order.Status,
        Items = order.Items.Select(i => new { i.Id, i.Sku, i.Quantity, i.UnitPrice }),
        Total = order.Total()
    });
});

app.Run();

