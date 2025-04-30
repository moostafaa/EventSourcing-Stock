using System;

namespace StockMarket.OMS.Messages
{
    public class MarketDataMessage : BaseMessage
    {
        public string Symbol { get; }
        public decimal LastPrice { get; }
        public decimal BidPrice { get; }
        public decimal AskPrice { get; }
        public decimal Volume { get; }
        public DateTime Timestamp { get; }

        public MarketDataMessage(
            string exchangeId,
            string symbol,
            decimal lastPrice,
            decimal bidPrice,
            decimal askPrice,
            decimal volume)
            : base(exchangeId, "MarketData")
        {
            Symbol = symbol;
            LastPrice = lastPrice;
            BidPrice = bidPrice;
            AskPrice = askPrice;
            Volume = volume;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class TradeMessage : BaseMessage
    {
        public string Symbol { get; }
        public decimal Price { get; }
        public decimal Quantity { get; }
        public DateTime TradeTime { get; }
        public string TradeId { get; }
        public bool IsBuyerMaker { get; }

        public TradeMessage(
            string exchangeId,
            string symbol,
            decimal price,
            decimal quantity,
            string tradeId,
            bool isBuyerMaker)
            : base(exchangeId, "Trade")
        {
            Symbol = symbol;
            Price = price;
            Quantity = quantity;
            TradeId = tradeId;
            IsBuyerMaker = isBuyerMaker;
            TradeTime = DateTime.UtcNow;
        }
    }

    public class OrderBookMessage : BaseMessage
    {
        public string Symbol { get; }
        public OrderBookLevel[] Bids { get; }
        public OrderBookLevel[] Asks { get; }
        public DateTime Timestamp { get; }

        public OrderBookMessage(
            string exchangeId,
            string symbol,
            OrderBookLevel[] bids,
            OrderBookLevel[] asks)
            : base(exchangeId, "OrderBook")
        {
            Symbol = symbol;
            Bids = bids;
            Asks = asks;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class OrderBookLevel
    {
        public decimal Price { get; }
        public decimal Quantity { get; }

        public OrderBookLevel(decimal price, decimal quantity)
        {
            Price = price;
            Quantity = quantity;
        }
    }
} 