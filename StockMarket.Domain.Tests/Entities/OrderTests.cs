using System;
using Xunit;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;
using StockMarket.Domain.Services;

namespace StockMarket.Domain.Tests.Entities
{
    public class OrderTests
    {
        [Fact]
        public void CreateOrder_WithValidParameters_ShouldCreateOrder()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var instrumentId = Guid.NewGuid();
            var quantity = new Quantity(100);
            var price = new Money(10.0m, "USD");
            var orderType = OrderType.Limit;
            var orderSide = OrderSide.Buy;
            var timeInForce = TimeInForce.GoodTillCancelled;

            // Act
            var order = Order.Create(
                accountId,
                instrumentId,
                quantity,
                price,
                orderType,
                orderSide,
                timeInForce);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(accountId, order.AccountId);
            Assert.Equal(instrumentId, order.InstrumentId);
            Assert.Equal(quantity, order.Quantity);
            Assert.Equal(price, order.Price);
            Assert.Equal(orderType, order.Type);
            Assert.Equal(orderSide, order.Side);
            Assert.Equal(timeInForce, order.TimeInForce);
            Assert.Equal(OrderStatus.Open, order.Status);
        }

        [Fact]
        public void EditOrder_WithValidParameters_ShouldUpdateOrder()
        {
            // Arrange
            var order = CreateTestOrder();
            var newQuantity = new Quantity(200);
            var newPrice = new Money(15.0m, "USD");

            // Act
            order.Edit(newQuantity, newPrice);

            // Assert
            Assert.Equal(newQuantity, order.Quantity);
            Assert.Equal(newPrice, order.Price);
        }

        [Fact]
        public void CancelOrder_WhenOpen_ShouldCancelOrder()
        {
            // Arrange
            var order = CreateTestOrder();

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);
        }

        [Fact]
        public void FillOrder_WithValidQuantity_ShouldUpdateOrder()
        {
            // Arrange
            var order = CreateTestOrder();
            var fillQuantity = new Quantity(50);

            // Act
            order.Fill(fillQuantity);

            // Assert
            Assert.Equal(fillQuantity, order.FilledQuantity);
            Assert.Equal(OrderStatus.PartiallyFilled, order.Status);
        }

        [Fact]
        public void FillOrder_WithFullQuantity_ShouldCompleteOrder()
        {
            // Arrange
            var order = CreateTestOrder();
            var fillQuantity = order.Quantity;

            // Act
            order.Fill(fillQuantity);

            // Assert
            Assert.Equal(fillQuantity, order.FilledQuantity);
            Assert.Equal(OrderStatus.Filled, order.Status);
        }

        [Fact]
        public void CalculatePrice_ShouldReturnCorrectCalculation()
        {
            // Arrange
            var order = CreateTestOrder();
            var commissionRate = 0.01m;

            // Act
            var priceCalculation = OrderPriceCalculation.Calculate(
                order.Price,
                order.Quantity,
                commissionRate);

            // Assert
            Assert.Equal(order.Price * order.Quantity.Value, priceCalculation.TotalPrice);
            Assert.Equal(priceCalculation.TotalPrice.Amount * commissionRate, priceCalculation.Commission.Amount);
            Assert.Equal(priceCalculation.TotalPrice + priceCalculation.Commission, priceCalculation.NetPrice);
        }

        private Order CreateTestOrder()
        {
            return Order.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Quantity(100),
                new Money(10.0m, "USD"),
                OrderType.Limit,
                OrderSide.Buy,
                TimeInForce.GoodTillCancelled);
        }
    }
} 