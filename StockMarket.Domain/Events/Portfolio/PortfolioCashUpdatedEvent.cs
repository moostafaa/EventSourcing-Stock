using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Portfolio
{
    public class PortfolioCashUpdatedEvent : DomainEvent
    {
        public Guid PortfolioId { get; }
        public decimal Amount { get; }

        public PortfolioCashUpdatedEvent(Guid portfolioId, decimal amount)
        {
            PortfolioId = portfolioId;
            Amount = amount;
        }
    }
} 