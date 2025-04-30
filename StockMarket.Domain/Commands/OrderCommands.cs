using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public OrderType Type { get; }
        public OrderSide Side { get; }
        public decimal Quantity { get; }
        public decimal? Price { get; }
        public TimeInForce TimeInForce { get; }

        public CreateOrderCommand(
            Guid accountId,
            Guid instrumentId,
            OrderType type,
            OrderSide side,
            decimal quantity,
            decimal? price,
            TimeInForce timeInForce)
        {
            AccountId = accountId;
            InstrumentId = instrumentId;
            Type = type;
            Side = side;
            Quantity = quantity;
            Price = price;
            TimeInForce = timeInForce;
        }
    }

    public class EditOrderCommand : ICommand
    {
        public Guid OrderId { get; }
        public decimal? NewQuantity { get; }
        public decimal? NewPrice { get; }
        public TimeInForce? NewTimeInForce { get; }

        public EditOrderCommand(
            Guid orderId,
            decimal? newQuantity = null,
            decimal? newPrice = null,
            TimeInForce? newTimeInForce = null)
        {
            OrderId = orderId;
            NewQuantity = newQuantity;
            NewPrice = newPrice;
            NewTimeInForce = newTimeInForce;
        }
    }

    public class CancelOrderCommand : ICommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }

        public CancelOrderCommand(
            Guid orderId,
            string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }

    public class ExecuteOrderCommand : ICommand
    {
        public Guid OrderId { get; }
        public decimal ExecutionPrice { get; }
        public decimal ExecutionQuantity { get; }

        public ExecuteOrderCommand(
            Guid orderId,
            decimal executionPrice,
            decimal executionQuantity)
        {
            OrderId = orderId;
            ExecutionPrice = executionPrice;
            ExecutionQuantity = executionQuantity;
        }
    }
} 