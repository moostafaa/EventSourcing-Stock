using System;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Services
{
    public class OrderPriceCalculation
    {
        public Money TotalPrice { get; }
        public Money Commission { get; }
        public Money NetPrice { get; }

        public OrderPriceCalculation(Money totalPrice, Money commission)
        {
            TotalPrice = totalPrice;
            Commission = commission;
            NetPrice = totalPrice + commission;
        }

        public static OrderPriceCalculation Calculate(Money price, Quantity quantity, decimal commissionRate)
        {
            var totalPrice = price * quantity.Value;
            var commission = new Money(totalPrice.Amount * commissionRate, totalPrice.Currency);
            return new OrderPriceCalculation(totalPrice, commission);
        }
    }
} 