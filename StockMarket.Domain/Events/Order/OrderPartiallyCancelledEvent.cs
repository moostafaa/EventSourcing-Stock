using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Order
{
    public class OrderPartiallyCancelledEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Quantity CancelledQuantity { get; }
        public string Reason { get; }

        public OrderPartiallyCancelledEvent(Guid orderId, Quantity cancelledQuantity, string reason)
        {
            OrderId = orderId;
            CancelledQuantity = cancelledQuantity;
            Reason = reason;
        }
    }
} 