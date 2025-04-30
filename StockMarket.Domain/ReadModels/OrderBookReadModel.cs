using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.Repositories;

namespace StockMarket.Domain.ReadModels
{
    public class OrderBookReadModel
    {
        private readonly IReadModelRepository<OrderBookEntry> _repository;

        public OrderBookReadModel(IReadModelRepository<OrderBookEntry> repository)
        {
            _repository = repository;
        }

        public async Task AddOrder(Guid orderId, Guid instrumentId, OrderSide side, decimal price, decimal quantity)
        {
            var entry = new OrderBookEntry
            {
                OrderId = orderId,
                InstrumentId = instrumentId,
                Side = side,
                Price = price,
                Quantity = quantity,
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveAsync(entry);
        }

        public async Task UpdateOrder(Guid orderId, decimal price, decimal quantity)
        {
            var entry = await _repository.GetByIdAsync(orderId);
            if (entry != null)
            {
                entry.Price = price;
                entry.Quantity = quantity;
                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task RemoveOrder(Guid orderId)
        {
            await _repository.DeleteAsync(orderId);
        }

        public async Task<IEnumerable<OrderBookEntry>> GetOrdersByInstrument(Guid instrumentId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.InstrumentId == instrumentId);
        }

        public async Task<IEnumerable<OrderBookEntry>> GetBuyOrdersByInstrument(Guid instrumentId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.InstrumentId == instrumentId && e.Side == OrderSide.Buy);
        }

        public async Task<IEnumerable<OrderBookEntry>> GetSellOrdersByInstrument(Guid instrumentId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.InstrumentId == instrumentId && e.Side == OrderSide.Sell);
        }
    }

    public class OrderBookEntry
    {
        public Guid OrderId { get; set; }
        public Guid InstrumentId { get; set; }
        public OrderSide Side { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 