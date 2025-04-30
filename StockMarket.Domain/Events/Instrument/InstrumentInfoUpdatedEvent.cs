using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Instrument
{
    public class InstrumentInfoUpdatedEvent : DomainEvent
    {
        public Guid InstrumentId { get; }

        public InstrumentInfoUpdatedEvent(Guid instrumentId)
        {
            InstrumentId = instrumentId;
        }
    }
} 