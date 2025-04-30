using System;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateOrderCommand
    {
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public OrderType Type { get; }
        public OrderSide Side { get; }
        public Money Price { get; }
        public Quantity Quantity { get; }

        public CreateOrderCommand(
            Guid accountId,
            Guid instrumentId,
            OrderType type,
            OrderSide side,
            Money price,
            Quantity quantity)
        {
            AccountId = accountId;
            InstrumentId = instrumentId;
            Type = type;
            Side = side;
            Price = price;
            Quantity = quantity;
        }
    }
} 