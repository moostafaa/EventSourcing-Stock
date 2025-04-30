using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMarket.Domain.Common;
using StockMarket.Domain.EventStore;
using StockMarket.Infrastructure.Data;

namespace StockMarket.Infrastructure.EventStore
{
    public class EventStore : IEventStore
    {
        private readonly ApplicationDbContext _context;

        public EventStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion)
        {
            var eventEntities = events.Select(e => new EventEntity
            {
                Id = e.Id,
                AggregateId = aggregateId,
                EventType = e.GetType().Name,
                EventData = System.Text.Json.JsonSerializer.Serialize(e),
                Version = expectedVersion + 1,
                OccurredOn = e.OccurredOn
            });

            await _context.Events.AddRangeAsync(eventEntities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            var events = await _context.Events
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Version)
                .ToListAsync();

            return events.Select(e => System.Text.Json.JsonSerializer.Deserialize<DomainEvent>(e.EventData)).ToList();
        }

        public async Task<List<DomainEvent>> GetAllEventsAsync()
        {
            var events = await _context.Events
                .OrderBy(e => e.OccurredOn)
                .ToListAsync();

            return events.Select(e => System.Text.Json.JsonSerializer.Deserialize<DomainEvent>(e.EventData)).ToList();
        }
    }
} 