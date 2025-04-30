using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Portfolio;

namespace StockMarket.Domain.Entities
{
    public class Portfolio : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public decimal AvailableCash { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private readonly List<PortfolioItem> _items = new List<PortfolioItem>();
        public IReadOnlyCollection<PortfolioItem> Items => _items.AsReadOnly();

        private Portfolio() { }

        public Portfolio(Guid accountId, decimal initialCash)
        {
            AccountId = accountId;
            AvailableCash = initialCash;
            TotalValue = initialCash;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new PortfolioCreatedEvent(Id, accountId, initialCash));
        }

        public void AddInstrument(Guid instrumentId, decimal quantity, decimal averagePrice)
        {
            var existingItem = _items.Find(i => i.InstrumentId == instrumentId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(quantity, averagePrice);
            }
            else
            {
                var newItem = new PortfolioItem(instrumentId, quantity, averagePrice);
                _items.Add(newItem);
            }

            UpdateTotalValue();
            AddDomainEvent(new PortfolioItemAddedEvent(Id, instrumentId, quantity, averagePrice));
        }

        public void RemoveInstrument(Guid instrumentId, decimal quantity)
        {
            var item = _items.Find(i => i.InstrumentId == instrumentId);
            if (item == null)
                throw new ArgumentException("Instrument not found in portfolio");

            if (quantity > item.Quantity)
                throw new ArgumentException("Insufficient quantity");

            item.UpdateQuantity(-quantity);
            if (item.Quantity == 0)
            {
                _items.Remove(item);
            }

            UpdateTotalValue();
            AddDomainEvent(new PortfolioItemRemovedEvent(Id, instrumentId));
        }

        public void UpdateCash(decimal amount)
        {
            AvailableCash += amount;
            TotalValue += amount;
            LastUpdated = DateTime.UtcNow;

            AddDomainEvent(new PortfolioCashUpdatedEvent(Id, amount));
        }

        private void UpdateTotalValue()
        {
            TotalValue = AvailableCash + _items.Sum(i => i.TotalValue);
            LastUpdated = DateTime.UtcNow;
        }
    }

    public class PortfolioItem
    {
        public Guid InstrumentId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal AveragePrice { get; private set; }
        public decimal TotalValue => Quantity * AveragePrice;

        public PortfolioItem(Guid instrumentId, decimal quantity, decimal averagePrice)
        {
            InstrumentId = instrumentId;
            Quantity = quantity;
            AveragePrice = averagePrice;
        }

        public void UpdateQuantity(decimal quantityChange, decimal newPrice = 0)
        {
            if (quantityChange == 0)
                return;

            var newQuantity = Quantity + quantityChange;
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative");

            if (quantityChange > 0 && newPrice > 0)
            {
                // Calculate new average price when buying
                AveragePrice = ((Quantity * AveragePrice) + (quantityChange * newPrice)) / newQuantity;
            }

            Quantity = newQuantity;
        }
    }
} 