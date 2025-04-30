using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Instrument;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.Handlers
{
    public class InstrumentEventHandlers
    {
        private readonly IReadModelRepository<OrderBookProjection> _orderBookRepository;
        private readonly IReadModelRepository<PortfolioReadModel> _portfolioRepository;

        public InstrumentEventHandlers(
            IReadModelRepository<OrderBookProjection> orderBookRepository,
            IReadModelRepository<PortfolioReadModel> portfolioRepository)
        {
            _orderBookRepository = orderBookRepository ?? throw new ArgumentNullException(nameof(orderBookRepository));
            _portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
        }

        public async Task Handle(InstrumentCreatedEvent @event)
        {
            var orderBook = new OrderBookProjection(@event.AggregateId);
            await _orderBookRepository.Save(@event.AggregateId, orderBook);
        }

        public async Task Handle(InstrumentPriceUpdatedEvent @event)
        {
            // Update all portfolios that contain this instrument
            var portfolios = await _portfolioRepository.GetAll();
            foreach (var portfolio in portfolios)
            {
                if (portfolio.Items.Any(i => i.InstrumentId == @event.AggregateId))
                {
                    portfolio.UpdateInstrumentPrice(@event.AggregateId, @event.NewPrice);
                    await _portfolioRepository.Save(portfolio.AccountId, portfolio);
                }
            }
        }

        public async Task Handle(InstrumentVolumeUpdatedEvent @event)
        {
            // This event is mainly for tracking and doesn't require immediate read model updates
            // Could be used for analytics or reporting purposes
        }
    }
} 