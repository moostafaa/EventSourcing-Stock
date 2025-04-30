using System;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.EventStore
{
    public class TradeRepository : EventSourcingRepository<Trade>
    {
        public TradeRepository(IEventStore eventStore) : base(eventStore)
        {
        }

        protected override bool ShouldCreateSnapshot(Trade aggregate)
        {
            // Create a snapshot every 50 events to optimize performance
            return aggregate.Version % 50 == 0;
        }

        public async Task<Trade> GetByIdWithSnapshotAsync(Guid id)
        {
            // Try to load from snapshot first
            var snapshot = await _eventStore.GetSnapshotAsync<Trade>(id);
            if (snapshot != null)
            {
                var events = await _eventStore.GetEventsAsync(id, snapshot.Version);
                snapshot.LoadFromHistory(events);
                return snapshot;
            }

            // If no snapshot exists, load from events
            return await GetByIdAsync(id);
        }
    }
} 