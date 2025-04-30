using System;

namespace StockMarket.Domain.ValueObjects
{
    public class Discount : IEquatable<Discount>
    {
        public decimal Rate { get; }
        public Money MaximumAmount { get; }

        public Discount(decimal rate, Money maximumAmount)
        {
            if (rate < 0 || rate > 1)
                throw new ArgumentException("Discount rate must be between 0 and 1", nameof(rate));
            if (maximumAmount == null)
                throw new ArgumentNullException(nameof(maximumAmount));

            Rate = rate;
            MaximumAmount = maximumAmount;
        }

        public Money Calculate(Money orderValue)
        {
            if (orderValue == null)
                throw new ArgumentNullException(nameof(orderValue));
            if (orderValue.Currency != MaximumAmount.Currency)
                throw new ArgumentException("Order value currency must match discount currency");

            var discount = orderValue * Rate;
            return discount > MaximumAmount ? MaximumAmount : discount;
        }

        public bool Equals(Discount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Rate == other.Rate && MaximumAmount.Equals(other.MaximumAmount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Discount)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rate, MaximumAmount);
        }
    }
} 