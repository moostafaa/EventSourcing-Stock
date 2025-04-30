using System;

namespace StockMarket.Domain.ValueObjects
{
    public class Quantity
    {
        public decimal Value { get; }

        public Quantity(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(value));

            Value = value;
        }

        public static Quantity operator +(Quantity left, Quantity right)
        {
            return new Quantity(left.Value + right.Value);
        }

        public static Quantity operator -(Quantity left, Quantity right)
        {
            if (left.Value < right.Value)
                throw new InvalidOperationException("Cannot subtract a larger quantity from a smaller one");

            return new Quantity(left.Value - right.Value);
        }

        public static Quantity operator *(Quantity quantity, decimal multiplier)
        {
            return new Quantity(quantity.Value * multiplier);
        }

        public static Quantity operator *(decimal multiplier, Quantity quantity)
        {
            return quantity * multiplier;
        }

        public override bool Equals(object obj)
        {
            if (obj is Quantity other)
            {
                return Value == other.Value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
} 