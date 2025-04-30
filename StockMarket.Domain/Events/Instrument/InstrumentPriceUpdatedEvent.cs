using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Instrument
{
    public class InstrumentPriceUpdatedEvent : DomainEvent
    {
        public Guid InstrumentId { get; }
        public string Symbol { get; }
        public decimal NewPrice { get; }

        public InstrumentPriceUpdatedEvent(Guid instrumentId, string symbol, decimal newPrice)
        {
            InstrumentId = instrumentId;
            Symbol = symbol;
            NewPrice = newPrice;
        }
    }
} 