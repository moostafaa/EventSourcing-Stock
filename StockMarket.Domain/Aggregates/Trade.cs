using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Trade;

namespace StockMarket.Domain.Entities
{
    public class Trade : AggregateRoot
    {
        public Guid BuyOrderId { get; private set; }
        public Guid SellOrderId { get; private set; }
        public Guid InstrumentId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public DateTime ExecutedAt { get; private set; }

        private Trade() { }

        public Trade(Guid buyOrderId, Guid sellOrderId, Guid instrumentId, decimal price, int quantity)
        {
            BuyOrderId = buyOrderId;
            SellOrderId = sellOrderId;
            InstrumentId = instrumentId;
            Price = price;
            Quantity = quantity;
            ExecutedAt = DateTime.UtcNow;

            AddDomainEvent(new TradeExecutedEvent(Id, buyOrderId, sellOrderId, instrumentId, price, quantity));
        }
    }
} 