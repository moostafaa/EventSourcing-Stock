using System;

namespace StockMarket.Domain.ValueObjects
{
    public class Commission : IEquatable<Commission>
    {
        public decimal Rate { get; }
        public Money MinimumAmount { get; }
        public Money MaximumAmount { get; }

        public Commission(decimal rate, Money minimumAmount, Money maximumAmount)
        {
            if (rate < 0)
                throw new ArgumentException("Commission rate cannot be negative", nameof(rate));
            if (minimumAmount == null)
                throw new ArgumentNullException(nameof(minimumAmount));
            if (maximumAmount == null)
                throw new ArgumentNullException(nameof(maximumAmount));
            if (minimumAmount.Currency != maximumAmount.Currency)
                throw new ArgumentException("Commission amounts must have the same currency");

            Rate = rate;
            MinimumAmount = minimumAmount;
            MaximumAmount = maximumAmount;
        }

        public Money Calculate(Money orderValue)
        {
            if (orderValue == null)
                throw new ArgumentNullException(nameof(orderValue));
            if (orderValue.Currency != MinimumAmount.Currency)
                throw new ArgumentException("Order value currency must match commission currency");

            var commission = orderValue * Rate;
            if (commission < MinimumAmount)
                return MinimumAmount;
            if (commission > MaximumAmount)
                return MaximumAmount;
            return commission;
        }

        public bool Equals(Commission other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Rate == other.Rate && 
                   MinimumAmount.Equals(other.MinimumAmount) && 
                   MaximumAmount.Equals(other.MaximumAmount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Commission)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rate, MinimumAmount, MaximumAmount);
        }
    }
} 