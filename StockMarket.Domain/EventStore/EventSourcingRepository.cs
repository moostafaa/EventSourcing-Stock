using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Domain.Common;

namespace StockMarket.Domain.EventStore
{
    public abstract class EventSourcingRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        protected readonly IEventStore _eventStore;

        protected EventSourcingRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<TAggregate> GetByIdAsync(Guid id)
        {
            var aggregate = new TAggregate();
            var events = await _eventStore.GetEventsAsync(id);
            
            if (!events.Any())
            {
                return null;
            }

            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            var uncommittedEvents = aggregate.GetUncommittedChanges();
            var version = aggregate.Version - uncommittedEvents.Count();

            await _eventStore.SaveEventsAsync(aggregate.Id, uncommittedEvents, version);
            aggregate.MarkChangesAsCommitted();

            // Save snapshot if needed
            if (ShouldCreateSnapshot(aggregate))
            {
                await _eventStore.SaveSnapshotAsync(aggregate.Id, aggregate, aggregate.Version);
            }
        }

        protected virtual bool ShouldCreateSnapshot(TAggregate aggregate)
        {
            // Default implementation - override in derived classes if needed
            return false;
        }
    }
} 