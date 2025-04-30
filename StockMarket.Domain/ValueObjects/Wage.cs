using System;

namespace StockMarket.Domain.ValueObjects
{
    public class Wage : IEquatable<Wage>
    {
        public Money FixedAmount { get; }
        public decimal Rate { get; }

        public Wage(Money fixedAmount, decimal rate)
        {
            if (fixedAmount == null)
                throw new ArgumentNullException(nameof(fixedAmount));
            if (rate < 0)
                throw new ArgumentException("Wage rate cannot be negative", nameof(rate));

            FixedAmount = fixedAmount;
            Rate = rate;
        }

        public Money Calculate(Money orderValue)
        {
            if (orderValue == null)
                throw new ArgumentNullException(nameof(orderValue));
            if (orderValue.Currency != FixedAmount.Currency)
                throw new ArgumentException("Order value currency must match wage currency");

            return FixedAmount + (orderValue * Rate);
        }

        public bool Equals(Wage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FixedAmount.Equals(other.FixedAmount) && Rate == other.Rate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Wage)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FixedAmount, Rate);
        }
    }
} 