using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Order
{
    public class OrderFilledEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public int FilledQuantity { get; }

        public OrderFilledEvent(Guid orderId, int filledQuantity)
        {
            OrderId = orderId;
            FilledQuantity = filledQuantity;
        }
    }
} 