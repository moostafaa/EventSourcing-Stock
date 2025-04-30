using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.Services;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Order
{
    public class OrderCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public OrderType Type { get; }
        public OrderSide Side { get; }
        public Money Price { get; }
        public Quantity Quantity { get; }
        public DateTime CreatedAt { get; }
        public OrderPriceCalculation PriceCalculation { get; }

        public OrderCreatedEvent(
            Guid orderId,
            Guid accountId,
            Guid instrumentId,
            OrderType type,
            OrderSide side,
            Money price,
            Quantity quantity,
            DateTime createdAt,
            OrderPriceCalculation priceCalculation)
            : base(orderId)
        {
            AccountId = accountId;
            InstrumentId = instrumentId;
            Type = type;
            Side = side;
            Price = price;
            Quantity = quantity;
            CreatedAt = createdAt;
            PriceCalculation = priceCalculation;
        }
    }
} 