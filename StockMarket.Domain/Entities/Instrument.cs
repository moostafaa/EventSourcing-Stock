using System;
using StockMarket.Domain.Common;

namespace StockMarket.Domain.Entities
{
    public class Instrument : AggregateRoot
    {
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public InstrumentType Type { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal OpeningPrice { get; private set; }
        public decimal HighPrice { get; private set; }
        public decimal LowPrice { get; private set; }
        public int Volume { get; private set; }
        public DateTime LastUpdated { get; private set; }

        // Stock specific properties
        public string? ISIN { get; private set; }
        public string? Exchange { get; private set; }
        public string? Sector { get; private set; }

        // Option specific properties
        public decimal? StrikePrice { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public OptionType? OptionType { get; private set; }

        // Future specific properties
        public DateTime? ContractExpiration { get; private set; }
        public decimal? ContractSize { get; private set; }

        private Instrument() { }

        public Instrument(string symbol, string name, InstrumentType type, decimal initialPrice)
        {
            Symbol = symbol;
            Name = name;
            Type = type;
            CurrentPrice = initialPrice;
            OpeningPrice = initialPrice;
            HighPrice = initialPrice;
            LowPrice = initialPrice;
            Volume = 0;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new InstrumentCreatedEvent(Id, symbol, name, type, initialPrice));
        }

        public void UpdateStockInfo(string isin, string exchange, string sector)
        {
            if (Type != InstrumentType.Stock)
                throw new InvalidOperationException("Cannot set stock information for non-stock instrument");

            ISIN = isin;
            Exchange = exchange;
            Sector = sector;

            AddDomainEvent(new InstrumentInfoUpdatedEvent(Id));
        }

        public void UpdateOptionInfo(decimal strikePrice, DateTime expirationDate, OptionType optionType)
        {
            if (Type != InstrumentType.Option)
                throw new InvalidOperationException("Cannot set option information for non-option instrument");

            StrikePrice = strikePrice;
            ExpirationDate = expirationDate;
            OptionType = optionType;

            AddDomainEvent(new InstrumentInfoUpdatedEvent(Id));
        }

        public void UpdateFutureInfo(DateTime contractExpiration, decimal contractSize)
        {
            if (Type != InstrumentType.Future)
                throw new InvalidOperationException("Cannot set future information for non-future instrument");

            ContractExpiration = contractExpiration;
            ContractSize = contractSize;

            AddDomainEvent(new InstrumentInfoUpdatedEvent(Id));
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than zero");

            CurrentPrice = newPrice;
            HighPrice = Math.Max(HighPrice, newPrice);
            LowPrice = Math.Min(LowPrice, newPrice);
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new InstrumentPriceUpdatedEvent(Id, Symbol, newPrice));
        }

        public void AddVolume(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            Volume += quantity;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new InstrumentVolumeUpdatedEvent(Id, Symbol, Volume));
        }
    }

    public enum InstrumentType
    {
        Stock,
        Option,
        Future,
        Bond,
        ETF
    }

    public enum OptionType
    {
        Call,
        Put
    }
} 