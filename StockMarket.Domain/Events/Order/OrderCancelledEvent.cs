using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Order
{
    public class OrderCancelledEvent : DomainEvent
    {
        public Guid OrderId { get; }

        public OrderCancelledEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
} 