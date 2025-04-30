using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Order
{
    public class OrderEditedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Money OldPrice { get; }
        public Quantity OldQuantity { get; }
        public Money NewPrice { get; }
        public Quantity NewQuantity { get; }

        public OrderEditedEvent(
            Guid orderId,
            Money oldPrice,
            Quantity oldQuantity,
            Money newPrice,
            Quantity newQuantity)
        {
            OrderId = orderId;
            OldPrice = oldPrice;
            OldQuantity = oldQuantity;
            NewPrice = newPrice;
            NewQuantity = newQuantity;
        }
    }
} 