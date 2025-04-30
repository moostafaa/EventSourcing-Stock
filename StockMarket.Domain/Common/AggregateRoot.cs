using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.Common;

namespace StockMarket.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        protected void ApplyChange(IDomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IDomainEvent @event, bool isNew)
        {
            dynamic d = this;
            d.Handle(Convert.ChangeType(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
                Version++;
            }
        }
    }
} 