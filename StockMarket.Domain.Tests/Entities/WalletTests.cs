using System;
using Xunit;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Tests.Entities
{
    public class WalletTests
    {
        [Fact]
        public void CreateWallet_WithValidParameters_ShouldCreateWallet()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var currency = "USD";

            // Act
            var wallet = Wallet.Create(accountId, currency);

            // Assert
            Assert.NotNull(wallet);
            Assert.Equal(accountId, wallet.AccountId);
            Assert.Equal(currency, wallet.Currency);
            Assert.Equal(0m, wallet.Balance);
            Assert.Empty(wallet.Transactions);
            Assert.Empty(wallet.BlockedAmounts);
        }

        [Fact]
        public void Deposit_WithValidAmount_ShouldUpdateBalance()
        {
            // Arrange
            var wallet = CreateTestWallet();
            var amount = new Money(100.0m, wallet.Currency);

            // Act
            wallet.Deposit(amount);

            // Assert
            Assert.Equal(amount.Amount, wallet.Balance);
            Assert.Single(wallet.Transactions);
            var transaction = wallet.Transactions[0];
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(WalletTransactionType.Deposit, transaction.Type);
        }

        [Fact]
        public void Withdraw_WithValidAmount_ShouldUpdateBalance()
        {
            // Arrange
            var wallet = CreateTestWallet();
            var depositAmount = new Money(200.0m, wallet.Currency);
            var withdrawAmount = new Money(100.0m, wallet.Currency);
            wallet.Deposit(depositAmount);

            // Act
            wallet.Withdraw(withdrawAmount);

            // Assert
            Assert.Equal(depositAmount.Amount - withdrawAmount.Amount, wallet.Balance);
            Assert.Equal(2, wallet.Transactions.Count);
            var transaction = wallet.Transactions[1];
            Assert.Equal(withdrawAmount, transaction.Amount);
            Assert.Equal(WalletTransactionType.Withdrawal, transaction.Type);
        }

        [Fact]
        public void BlockAmount_WithValidAmount_ShouldBlockAmount()
        {
            // Arrange
            var wallet = CreateTestWallet();
            var depositAmount = new Money(200.0m, wallet.Currency);
            var blockAmount = new Money(100.0m, wallet.Currency);
            var reason = "Order execution";
            wallet.Deposit(depositAmount);

            // Act
            wallet.BlockAmount(blockAmount, reason);

            // Assert
            Assert.Equal(depositAmount.Amount - blockAmount.Amount, wallet.AvailableBalance);
            Assert.Single(wallet.BlockedAmounts);
            var blockedAmount = wallet.BlockedAmounts[0];
            Assert.Equal(blockAmount, blockedAmount.Amount);
            Assert.Equal(reason, blockedAmount.Reason);
        }

        [Fact]
        public void UnblockAmount_WithValidAmount_ShouldUnblockAmount()
        {
            // Arrange
            var wallet = CreateTestWallet();
            var depositAmount = new Money(200.0m, wallet.Currency);
            var blockAmount = new Money(100.0m, wallet.Currency);
            var reason = "Order execution";
            wallet.Deposit(depositAmount);
            wallet.BlockAmount(blockAmount, reason);

            // Act
            wallet.UnblockAmount(blockAmount, reason);

            // Assert
            Assert.Equal(depositAmount.Amount, wallet.AvailableBalance);
            Assert.Empty(wallet.BlockedAmounts);
        }

        private Wallet CreateTestWallet()
        {
            return Wallet.Create(Guid.NewGuid(), "USD");
        }
    }
} 