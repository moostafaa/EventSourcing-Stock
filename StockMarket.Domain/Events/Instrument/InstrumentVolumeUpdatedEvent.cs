using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Instrument
{
    public class InstrumentVolumeUpdatedEvent : DomainEvent
    {
        public Guid InstrumentId { get; }
        public string Symbol { get; }
        public int NewVolume { get; }

        public InstrumentVolumeUpdatedEvent(Guid instrumentId, string symbol, int newVolume)
        {
            InstrumentId = instrumentId;
            Symbol = symbol;
            NewVolume = newVolume;
        }
    }
} 