using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Common;

namespace StockMarket.Domain.EventStore
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events, int expectedVersion);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId, int fromVersion);
        Task<int> GetLastVersionAsync(Guid aggregateId);
        Task SaveSnapshotAsync(Guid aggregateId, object snapshot, int version);
        Task<T> GetSnapshotAsync<T>(Guid aggregateId) where T : class;
    }
} 