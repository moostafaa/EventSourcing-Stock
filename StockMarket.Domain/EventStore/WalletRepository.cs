using System;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.EventStore
{
    public class WalletRepository : EventSourcingRepository<Wallet>
    {
        public WalletRepository(IEventStore eventStore) : base(eventStore)
        {
        }

        protected override bool ShouldCreateSnapshot(Wallet aggregate)
        {
            // Create a snapshot every 50 events to optimize performance
            return aggregate.Version % 50 == 0;
        }

        public async Task<Wallet> GetByIdWithSnapshotAsync(Guid id)
        {
            // Try to load from snapshot first
            var snapshot = await _eventStore.GetSnapshotAsync<Wallet>(id);
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