using System;
using Xunit;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Tests.ValueObjects
{
    public class QuantityTests
    {
        [Fact]
        public void CreateQuantity_WithValidValue_ShouldCreateQuantity()
        {
            // Arrange
            var value = 100.0m;

            // Act
            var quantity = new Quantity(value);

            // Assert
            Assert.Equal(value, quantity.Value);
        }

        [Fact]
        public void CreateQuantity_WithZeroValue_ShouldThrowException()
        {
            // Arrange
            var value = 0.0m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Quantity(value));
        }

        [Fact]
        public void CreateQuantity_WithNegativeValue_ShouldThrowException()
        {
            // Arrange
            var value = -100.0m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Quantity(value));
        }

        [Fact]
        public void AddQuantities_ShouldReturnCorrectSum()
        {
            // Arrange
            var quantity1 = new Quantity(100.0m);
            var quantity2 = new Quantity(50.0m);

            // Act
            var result = quantity1 + quantity2;

            // Assert
            Assert.Equal(150.0m, result.Value);
        }

        [Fact]
        public void SubtractQuantities_ShouldReturnCorrectDifference()
        {
            // Arrange
            var quantity1 = new Quantity(100.0m);
            var quantity2 = new Quantity(50.0m);

            // Act
            var result = quantity1 - quantity2;

            // Assert
            Assert.Equal(50.0m, result.Value);
        }

        [Fact]
        public void SubtractQuantities_WithLargerQuantity_ShouldThrowException()
        {
            // Arrange
            var quantity1 = new Quantity(50.0m);
            var quantity2 = new Quantity(100.0m);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => quantity1 - quantity2);
        }

        [Fact]
        public void MultiplyQuantity_WithDecimal_ShouldReturnCorrectProduct()
        {
            // Arrange
            var quantity = new Quantity(100.0m);
            var multiplier = 2.5m;

            // Act
            var result = quantity * multiplier;

            // Assert
            Assert.Equal(250.0m, result.Value);
        }

        [Fact]
        public void Equals_WithSameValues_ShouldReturnTrue()
        {
            // Arrange
            var quantity1 = new Quantity(100.0m);
            var quantity2 = new Quantity(100.0m);

            // Act & Assert
            Assert.True(quantity1.Equals(quantity2));
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldReturnFalse()
        {
            // Arrange
            var quantity1 = new Quantity(100.0m);
            var quantity2 = new Quantity(50.0m);

            // Act & Assert
            Assert.False(quantity1.Equals(quantity2));
        }
    }
} 