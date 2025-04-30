using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Portfolio
{
    public class PortfolioItemRemovedEvent : DomainEvent
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }
        public int Quantity { get; }

        public PortfolioItemRemovedEvent(Guid portfolioId, Guid instrumentId, int quantity)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
            Quantity = quantity;
        }
    }
} 