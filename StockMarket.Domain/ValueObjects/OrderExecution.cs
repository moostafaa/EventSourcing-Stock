using System;

namespace StockMarket.Domain.ValueObjects
{
    public class OrderExecution : IEquatable<OrderExecution>
    {
        public Quantity Quantity { get; }
        public Money Price { get; }
        public DateTime ExecutedAt { get; }

        public OrderExecution(Quantity quantity, Money price, DateTime executedAt)
        {
            if (quantity == null)
                throw new ArgumentNullException(nameof(quantity));
            if (price == null)
                throw new ArgumentNullException(nameof(price));

            Quantity = quantity;
            Price = price;
            ExecutedAt = executedAt;
        }

        public Money TotalValue => Price * Quantity.Value;

        public bool Equals(OrderExecution other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Quantity.Equals(other.Quantity) && 
                   Price.Equals(other.Price) && 
                   ExecutedAt == other.ExecutedAt;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OrderExecution)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Quantity, Price, ExecutedAt);
        }
    }
} 