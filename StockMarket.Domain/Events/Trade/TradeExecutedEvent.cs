using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Trade
{
    public class TradeExecutedEvent : DomainEvent
    {
        public Guid TradeId { get; }
        public Guid BuyOrderId { get; }
        public Guid SellOrderId { get; }
        public Guid InstrumentId { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public TradeExecutedEvent(Guid tradeId, Guid buyOrderId, Guid sellOrderId, Guid instrumentId, decimal price, int quantity)
        {
            TradeId = tradeId;
            BuyOrderId = buyOrderId;
            SellOrderId = sellOrderId;
            InstrumentId = instrumentId;
            Price = price;
            Quantity = quantity;
        }
    }
} 