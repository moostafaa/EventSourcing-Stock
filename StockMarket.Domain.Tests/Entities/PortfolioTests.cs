using System;
using Xunit;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Tests.Entities
{
    public class PortfolioTests
    {
        [Fact]
        public void CreatePortfolio_WithValidParameters_ShouldCreatePortfolio()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var name = "Test Portfolio";

            // Act
            var portfolio = Portfolio.Create(accountId, name);

            // Assert
            Assert.NotNull(portfolio);
            Assert.Equal(accountId, portfolio.AccountId);
            Assert.Equal(name, portfolio.Name);
            Assert.Empty(portfolio.Items);
        }

        [Fact]
        public void AddInstrument_WithValidParameters_ShouldAddInstrument()
        {
            // Arrange
            var portfolio = CreateTestPortfolio();
            var instrumentId = Guid.NewGuid();
            var quantity = new Quantity(100);

            // Act
            portfolio.AddInstrument(instrumentId, quantity);

            // Assert
            Assert.Single(portfolio.Items);
            var item = portfolio.Items[0];
            Assert.Equal(instrumentId, item.InstrumentId);
            Assert.Equal(quantity, item.Quantity);
        }

        [Fact]
        public void RemoveInstrument_WithValidParameters_ShouldRemoveInstrument()
        {
            // Arrange
            var portfolio = CreateTestPortfolio();
            var instrumentId = Guid.NewGuid();
            var quantity = new Quantity(100);
            portfolio.AddInstrument(instrumentId, quantity);

            // Act
            portfolio.RemoveInstrument(instrumentId, quantity);

            // Assert
            Assert.Empty(portfolio.Items);
        }

        [Fact]
        public void RemoveInstrument_WithPartialQuantity_ShouldUpdateQuantity()
        {
            // Arrange
            var portfolio = CreateTestPortfolio();
            var instrumentId = Guid.NewGuid();
            var initialQuantity = new Quantity(100);
            var removeQuantity = new Quantity(50);
            portfolio.AddInstrument(instrumentId, initialQuantity);

            // Act
            portfolio.RemoveInstrument(instrumentId, removeQuantity);

            // Assert
            Assert.Single(portfolio.Items);
            var item = portfolio.Items[0];
            Assert.Equal(instrumentId, item.InstrumentId);
            Assert.Equal(initialQuantity - removeQuantity, item.Quantity);
        }

        [Fact]
        public void UpdateInstrumentQuantity_WithValidParameters_ShouldUpdateQuantity()
        {
            // Arrange
            var portfolio = CreateTestPortfolio();
            var instrumentId = Guid.NewGuid();
            var initialQuantity = new Quantity(100);
            var newQuantity = new Quantity(200);
            portfolio.AddInstrument(instrumentId, initialQuantity);

            // Act
            portfolio.UpdateInstrumentQuantity(instrumentId, newQuantity);

            // Assert
            Assert.Single(portfolio.Items);
            var item = portfolio.Items[0];
            Assert.Equal(instrumentId, item.InstrumentId);
            Assert.Equal(newQuantity, item.Quantity);
        }

        private Portfolio CreateTestPortfolio()
        {
            return Portfolio.Create(Guid.NewGuid(), "Test Portfolio");
        }
    }
} 