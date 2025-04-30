using System;
using System.Threading.Tasks;
using Marten;
using StockMarket.Domain.Common;
using StockMarket.Domain.EventStore;

namespace StockMarket.Infrastructure.Repositories
{
    public class MartenRepository<TAggregate> : IEventSourcingRepository<TAggregate>
        where TAggregate : class, IAggregateRoot, new()
    {
        private readonly IDocumentSession _session;

        public MartenRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<TAggregate> GetByIdAsync(Guid id)
        {
            var aggregate = await _session.Events.AggregateStreamAsync<TAggregate>(id);
            return aggregate ?? throw new AggregateNotFoundException(id);
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            var events = aggregate.GetUncommittedEvents();
            if (events.Count == 0)
                return;

            _session.Events.Append(aggregate.Id, aggregate.Version, events);
            await _session.SaveChangesAsync();
            aggregate.ClearUncommittedEvents();
        }

        public async Task<TAggregate> GetByIdWithSnapshotAsync(Guid id)
        {
            var aggregate = await _session.Events.AggregateStreamAsync<TAggregate>(id);
            return aggregate ?? throw new AggregateNotFoundException(id);
        }

        public bool ShouldCreateSnapshot(TAggregate aggregate, int eventsSinceLastSnapshot)
        {
            return eventsSinceLastSnapshot >= 50;
        }
    }
} 