using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Repositories;

namespace StockMarket.Domain.ReadModels
{
    public class TradeReadModel
    {
        private readonly IReadModelRepository<TradeEntry> _repository;

        public TradeReadModel(IReadModelRepository<TradeEntry> repository)
        {
            _repository = repository;
        }

        public async Task AddTrade(
            Guid tradeId,
            Guid buyOrderId,
            Guid sellOrderId,
            Guid instrumentId,
            decimal price,
            decimal quantity,
            DateTime timestamp)
        {
            var entry = new TradeEntry
            {
                TradeId = tradeId,
                BuyOrderId = buyOrderId,
                SellOrderId = sellOrderId,
                InstrumentId = instrumentId,
                Price = price,
                Quantity = quantity,
                Timestamp = timestamp
            };

            await _repository.SaveAsync(entry);
        }

        public async Task<TradeEntry> GetTrade(Guid tradeId)
        {
            return await _repository.GetByIdAsync(tradeId);
        }

        public async Task<IEnumerable<TradeEntry>> GetTradesByInstrument(Guid instrumentId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.InstrumentId == instrumentId);
        }

        public async Task<IEnumerable<TradeEntry>> GetTradesByOrder(Guid orderId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.BuyOrderId == orderId || e.SellOrderId == orderId);
        }
    }

    public class TradeEntry
    {
        public Guid TradeId { get; set; }
        public Guid BuyOrderId { get; set; }
        public Guid SellOrderId { get; set; }
        public Guid InstrumentId { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 