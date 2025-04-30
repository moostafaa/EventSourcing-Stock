using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;
using StockMarket.Domain.Services;
using StockMarket.Domain.EventStore;

namespace StockMarket.API.Endpoints
{
    public static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/orders")
                .WithTags("Orders")
                .WithOpenApi();

            group.MapPost("/", async (
                CreateOrderRequest request,
                IOrderRepository orderRepository) =>
            {
                var order = Order.Create(
                    request.AccountId,
                    request.InstrumentId,
                    new Quantity(request.Quantity),
                    new Money(request.Price, request.Currency),
                    request.OrderType,
                    request.OrderSide,
                    request.TimeInForce);

                await orderRepository.SaveAsync(order);

                return Results.Created($"/api/orders/{order.Id}", new OrderResponse(order));
            })
            .WithName("CreateOrder")
            .WithDescription("Creates a new order")
            .Produces<OrderResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", async (
                Guid id,
                EditOrderRequest request,
                IOrderRepository orderRepository) =>
            {
                var order = await orderRepository.GetByIdAsync(id);
                order.Edit(
                    new Quantity(request.Quantity),
                    new Money(request.Price, request.Currency));

                await orderRepository.SaveAsync(order);

                return Results.Ok(new OrderResponse(order));
            })
            .WithName("EditOrder")
            .WithDescription("Edits an existing order")
            .Produces<OrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id}", async (
                Guid id,
                IOrderRepository orderRepository) =>
            {
                var order = await orderRepository.GetByIdAsync(id);
                order.Cancel();

                await orderRepository.SaveAsync(order);

                return Results.NoContent();
            })
            .WithName("CancelOrder")
            .WithDescription("Cancels an existing order")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/{id}", async (
                Guid id,
                IOrderRepository orderRepository) =>
            {
                var order = await orderRepository.GetByIdAsync(id);
                return Results.Ok(new OrderResponse(order));
            })
            .WithName("GetOrder")
            .WithDescription("Gets an order by ID")
            .Produces<OrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }

    public record CreateOrderRequest(
        Guid AccountId,
        Guid InstrumentId,
        decimal Quantity,
        decimal Price,
        string Currency,
        OrderType OrderType,
        OrderSide OrderSide,
        TimeInForce TimeInForce);

    public record EditOrderRequest(
        decimal Quantity,
        decimal Price,
        string Currency);

    public record OrderResponse(
        Guid Id,
        Guid AccountId,
        Guid InstrumentId,
        decimal Quantity,
        decimal Price,
        string Currency,
        OrderType Type,
        OrderSide Side,
        TimeInForce TimeInForce,
        OrderStatus Status,
        decimal FilledQuantity)
    {
        public OrderResponse(Order order) : this(
            order.Id,
            order.AccountId,
            order.InstrumentId,
            order.Quantity.Value,
            order.Price.Amount,
            order.Price.Currency,
            order.Type,
            order.Side,
            order.TimeInForce,
            order.Status,
            order.FilledQuantity.Value)
        {
        }
    }
} 