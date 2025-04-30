using System;
using StockMarket.OMS.Messages.Common;

namespace StockMarket.OMS.Messages.Orders
{
    public abstract class OrderMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }
        public string Currency { get; }
        public OrderType Type { get; }
        public OrderSide Side { get; }
        public TimeInForce TimeInForce { get; }

        protected OrderMessage(
            string messageType,
            Guid orderId,
            Guid accountId,
            Guid instrumentId,
            decimal quantity,
            decimal price,
            string currency,
            OrderType type,
            OrderSide side,
            TimeInForce timeInForce)
            : base(messageType)
        {
            OrderId = orderId;
            AccountId = accountId;
            InstrumentId = instrumentId;
            Quantity = quantity;
            Price = price;
            Currency = currency;
            Type = type;
            Side = side;
            TimeInForce = timeInForce;
        }
    }
} 