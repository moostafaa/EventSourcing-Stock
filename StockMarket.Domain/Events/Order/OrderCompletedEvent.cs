using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Order
{
    public class OrderCompletedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Quantity TotalFilledQuantity { get; }
        public Quantity TotalCancelledQuantity { get; }

        public OrderCompletedEvent(Guid orderId, Quantity totalFilledQuantity, Quantity totalCancelledQuantity)
        {
            OrderId = orderId;
            TotalFilledQuantity = totalFilledQuantity;
            TotalCancelledQuantity = totalCancelledQuantity;
        }
    }
} 