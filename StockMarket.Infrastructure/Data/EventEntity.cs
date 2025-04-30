using System;

namespace StockMarket.Infrastructure.Data
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
        public int Version { get; set; }
        public DateTime OccurredOn { get; set; }
    }
} 