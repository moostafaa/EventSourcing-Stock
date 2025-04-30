using System;
using StockMarket.Domain.Common;

namespace StockMarket.Domain.Entities
{
    public class Stock : AggregateRoot
    {
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal OpeningPrice { get; private set; }
        public decimal HighPrice { get; private set; }
        public decimal LowPrice { get; private set; }
        public int Volume { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private Stock() { }

        public Stock(string symbol, string name, decimal initialPrice)
        {
            Symbol = symbol;
            Name = name;
            CurrentPrice = initialPrice;
            OpeningPrice = initialPrice;
            HighPrice = initialPrice;
            LowPrice = initialPrice;
            Volume = 0;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new StockCreatedEvent(Id, symbol, name, initialPrice));
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than zero");

            CurrentPrice = newPrice;
            HighPrice = Math.Max(HighPrice, newPrice);
            LowPrice = Math.Min(LowPrice, newPrice);
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new StockPriceUpdatedEvent(Id, Symbol, newPrice));
        }

        public void AddVolume(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            Volume += quantity;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new StockVolumeUpdatedEvent(Id, Symbol, Volume));
        }
    }
} 