using System;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Services
{
    public class OrderPriceCalculator
    {
        private readonly Commission _commission;
        private readonly Wage _wage;
        private readonly Discount _discount;

        public OrderPriceCalculator(Commission commission, Wage wage, Discount discount)
        {
            _commission = commission ?? throw new ArgumentNullException(nameof(commission));
            _wage = wage ?? throw new ArgumentNullException(nameof(wage));
            _discount = discount ?? throw new ArgumentNullException(nameof(discount));
        }

        public OrderPriceCalculation Calculate(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var orderValue = order.Price * order.TotalQuantity.Value;
            var commission = _commission.Calculate(orderValue);
            var wage = _wage.Calculate(orderValue);
            var discount = _discount.Calculate(orderValue);

            var totalFees = commission + wage;
            var netFees = totalFees - discount;
            var totalAmount = orderValue + netFees;

            return new OrderPriceCalculation(
                orderValue,
                commission,
                wage,
                discount,
                netFees,
                totalAmount
            );
        }
    }

    public class OrderPriceCalculation
    {
        public Money OrderValue { get; }
        public Money Commission { get; }
        public Money Wage { get; }
        public Money Discount { get; }
        public Money NetFees { get; }
        public Money TotalAmount { get; }

        public OrderPriceCalculation(
            Money orderValue,
            Money commission,
            Money wage,
            Money discount,
            Money netFees,
            Money totalAmount)
        {
            OrderValue = orderValue;
            Commission = commission;
            Wage = wage;
            Discount = discount;
            NetFees = netFees;
            TotalAmount = totalAmount;
        }
    }
} 