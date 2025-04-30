using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Instrument
{
    public class InstrumentCreatedEvent : DomainEvent
    {
        public Guid InstrumentId { get; }
        public string Symbol { get; }
        public string Name { get; }
        public InstrumentType Type { get; }
        public decimal InitialPrice { get; }

        public InstrumentCreatedEvent(Guid instrumentId, string symbol, string name, InstrumentType type, decimal initialPrice)
        {
            InstrumentId = instrumentId;
            Symbol = symbol;
            Name = name;
            Type = type;
            InitialPrice = initialPrice;
        }
    }
} 