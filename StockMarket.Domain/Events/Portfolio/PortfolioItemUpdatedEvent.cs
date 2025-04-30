using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Portfolio
{
    public class PortfolioItemUpdatedEvent : DomainEvent
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }
        public decimal Quantity { get; }
        public decimal AveragePrice { get; }

        public PortfolioItemUpdatedEvent(Guid portfolioId, Guid instrumentId, decimal quantity, decimal averagePrice)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
            Quantity = quantity;
            AveragePrice = averagePrice;
        }
    }
} 