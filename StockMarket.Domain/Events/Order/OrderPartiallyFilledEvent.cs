using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Order
{
    public class OrderPartiallyFilledEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Quantity FilledQuantity { get; }
        public Money ExecutionPrice { get; }

        public OrderPartiallyFilledEvent(Guid orderId, Quantity filledQuantity, Money executionPrice)
        {
            OrderId = orderId;
            FilledQuantity = filledQuantity;
            ExecutionPrice = executionPrice;
        }
    }
} 