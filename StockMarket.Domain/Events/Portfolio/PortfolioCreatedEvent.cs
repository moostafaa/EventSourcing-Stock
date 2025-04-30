using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Portfolio
{
    public class PortfolioCreatedEvent : DomainEvent
    {
        public Guid PortfolioId { get; }
        public Guid AccountId { get; }
        public decimal InitialCash { get; }

        public PortfolioCreatedEvent(Guid portfolioId, Guid accountId, decimal initialCash)
        {
            PortfolioId = portfolioId;
            AccountId = accountId;
            InitialCash = initialCash;
        }
    }
} 