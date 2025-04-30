using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Domain.ValueObjects;
using StockMarket.Domain.Repositories;

namespace StockMarket.Domain.ReadModels
{
    public class PortfolioReadModel
    {
        private readonly IReadModelRepository<PortfolioEntry> _repository;

        public PortfolioReadModel(IReadModelRepository<PortfolioEntry> repository)
        {
            _repository = repository;
        }

        public async Task AddPortfolio(Guid portfolioId, Guid accountId)
        {
            var entry = new PortfolioEntry
            {
                PortfolioId = portfolioId,
                AccountId = accountId,
                Instruments = new Dictionary<Guid, decimal>(),
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveAsync(entry);
        }

        public async Task AddInstrument(Guid portfolioId, Guid instrumentId, decimal quantity)
        {
            var entry = await _repository.GetByIdAsync(portfolioId);
            if (entry != null)
            {
                if (entry.Instruments.ContainsKey(instrumentId))
                {
                    entry.Instruments[instrumentId] += quantity;
                }
                else
                {
                    entry.Instruments[instrumentId] = quantity;
                }

                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task RemoveInstrument(Guid portfolioId, Guid instrumentId, decimal quantity)
        {
            var entry = await _repository.GetByIdAsync(portfolioId);
            if (entry != null && entry.Instruments.ContainsKey(instrumentId))
            {
                entry.Instruments[instrumentId] -= quantity;
                if (entry.Instruments[instrumentId] <= 0)
                {
                    entry.Instruments.Remove(instrumentId);
                }

                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task<PortfolioEntry> GetPortfolio(Guid portfolioId)
        {
            return await _repository.GetByIdAsync(portfolioId);
        }

        public async Task<IEnumerable<PortfolioEntry>> GetPortfoliosByAccount(Guid accountId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.AccountId == accountId);
        }

        public class PortfolioItem
        {
            public Guid InstrumentId { get; }
            public Quantity Quantity { get; set; }
            public Money CurrentPrice { get; set; }

            public PortfolioItem(Guid instrumentId, Quantity quantity, Money currentPrice)
            {
                InstrumentId = instrumentId;
                Quantity = quantity;
                CurrentPrice = currentPrice;
            }
        }
    }

    public class PortfolioEntry
    {
        public Guid PortfolioId { get; set; }
        public Guid AccountId { get; set; }
        public Dictionary<Guid, decimal> Instruments { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 