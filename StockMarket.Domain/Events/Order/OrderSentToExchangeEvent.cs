using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Order
{
    public class OrderSentToExchangeEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public string ExchangeOrderId { get; }

        public OrderSentToExchangeEvent(Guid orderId, string exchangeOrderId)
        {
            OrderId = orderId;
            ExchangeOrderId = exchangeOrderId;
        }
    }
} 