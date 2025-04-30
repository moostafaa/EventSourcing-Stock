using System;
using Xunit;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Tests.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void CreateMoney_WithValidParameters_ShouldCreateMoney()
        {
            // Arrange
            var amount = 100.0m;
            var currency = "USD";

            // Act
            var money = new Money(amount, currency);

            // Assert
            Assert.Equal(amount, money.Amount);
            Assert.Equal(currency, money.Currency);
        }

        [Fact]
        public void CreateMoney_WithNegativeAmount_ShouldThrowException()
        {
            // Arrange
            var amount = -100.0m;
            var currency = "USD";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Money(amount, currency));
        }

        [Fact]
        public void CreateMoney_WithEmptyCurrency_ShouldThrowException()
        {
            // Arrange
            var amount = 100.0m;
            var currency = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Money(amount, currency));
        }

        [Fact]
        public void AddMoney_WithSameCurrency_ShouldReturnCorrectSum()
        {
            // Arrange
            var money1 = new Money(100.0m, "USD");
            var money2 = new Money(50.0m, "USD");

            // Act
            var result = money1 + money2;

            // Assert
            Assert.Equal(150.0m, result.Amount);
            Assert.Equal("USD", result.Currency);
        }

        [Fact]
        public void AddMoney_WithDifferentCurrencies_ShouldThrowException()
        {
            // Arrange
            var money1 = new Money(100.0m, "USD");
            var money2 = new Money(50.0m, "EUR");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => money1 + money2);
        }

        [Fact]
        public void SubtractMoney_WithSameCurrency_ShouldReturnCorrectDifference()
        {
            // Arrange
            var money1 = new Money(100.0m, "USD");
            var money2 = new Money(50.0m, "USD");

            // Act
            var result = money1 - money2;

            // Assert
            Assert.Equal(50.0m, result.Amount);
            Assert.Equal("USD", result.Currency);
        }

        [Fact]
        public void MultiplyMoney_WithDecimal_ShouldReturnCorrectProduct()
        {
            // Arrange
            var money = new Money(100.0m, "USD");
            var multiplier = 2.5m;

            // Act
            var result = money * multiplier;

            // Assert
            Assert.Equal(250.0m, result.Amount);
            Assert.Equal("USD", result.Currency);
        }

        [Fact]
        public void Equals_WithSameValues_ShouldReturnTrue()
        {
            // Arrange
            var money1 = new Money(100.0m, "USD");
            var money2 = new Money(100.0m, "USD");

            // Act & Assert
            Assert.True(money1.Equals(money2));
            Assert.True(money1 == money2);
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldReturnFalse()
        {
            // Arrange
            var money1 = new Money(100.0m, "USD");
            var money2 = new Money(50.0m, "USD");
            var money3 = new Money(100.0m, "EUR");

            // Act & Assert
            Assert.False(money1.Equals(money2));
            Assert.False(money1 == money2);
            Assert.False(money1.Equals(money3));
            Assert.False(money1 == money3);
        }
    }
} 