using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Portfolio;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.Handlers
{
    public class PortfolioEventHandlers
    {
        private readonly IReadModelRepository<PortfolioReadModel> _portfolioRepository;

        public PortfolioEventHandlers(IReadModelRepository<PortfolioReadModel> portfolioRepository)
        {
            _portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
        }

        public async Task Handle(PortfolioCreatedEvent @event)
        {
            var readModel = new PortfolioReadModel(@event.AccountId);
            await _portfolioRepository.Save(@event.AccountId, readModel);
        }

        public async Task Handle(InstrumentAddedEvent @event)
        {
            var readModel = await _portfolioRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.AddInstrument(@event.InstrumentId, @event.Quantity, @event.Price);
                await _portfolioRepository.Save(@event.AggregateId, readModel);
            }
        }

        public async Task Handle(InstrumentRemovedEvent @event)
        {
            var readModel = await _portfolioRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.RemoveInstrument(@event.InstrumentId, @event.Quantity);
                await _portfolioRepository.Save(@event.AggregateId, readModel);
            }
        }

        public async Task Handle(InstrumentPriceUpdatedEvent @event)
        {
            var readModel = await _portfolioRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.UpdateInstrumentPrice(@event.InstrumentId, @event.NewPrice);
                await _portfolioRepository.Save(@event.AggregateId, readModel);
            }
        }
    }
} 