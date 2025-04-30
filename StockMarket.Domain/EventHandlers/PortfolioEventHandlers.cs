using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Portfolio;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.EventHandlers
{
    public class PortfolioEventHandlers
    {
        private readonly PortfolioReadModel _portfolioReadModel;

        public PortfolioEventHandlers(PortfolioReadModel portfolioReadModel)
        {
            _portfolioReadModel = portfolioReadModel;
        }

        public Task Handle(PortfolioCreatedEvent @event)
        {
            _portfolioReadModel.AddPortfolio(@event.PortfolioId, @event.AccountId);
            return Task.CompletedTask;
        }

        public Task Handle(InstrumentAddedToPortfolioEvent @event)
        {
            _portfolioReadModel.AddInstrument(@event.PortfolioId, @event.InstrumentId, @event.Quantity);
            return Task.CompletedTask;
        }

        public Task Handle(InstrumentRemovedFromPortfolioEvent @event)
        {
            _portfolioReadModel.RemoveInstrument(@event.PortfolioId, @event.InstrumentId, @event.Quantity);
            return Task.CompletedTask;
        }
    }
} 