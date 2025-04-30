using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Portfolio
{
    public class PortfolioItemQuantityUpdatedEvent : DomainEvent
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }
        public int OldQuantity { get; }
        public int NewQuantity { get; }

        public PortfolioItemQuantityUpdatedEvent(Guid portfolioId, Guid instrumentId, int oldQuantity, int newQuantity)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }
} 