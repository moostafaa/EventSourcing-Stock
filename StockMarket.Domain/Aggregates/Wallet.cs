using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Wallet;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Aggregates
{
    public class Wallet : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public Money Balance { get; private set; }
        public Money BlockedAmount { get; private set; }
        public List<WalletTransaction> Transactions { get; private set; }

        private Wallet() { }

        public Wallet(Guid accountId, Money initialBalance)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("Account ID cannot be empty", nameof(accountId));
            if (initialBalance == null)
                throw new ArgumentNullException(nameof(initialBalance));

            AccountId = accountId;
            Balance = initialBalance;
            BlockedAmount = new Money(0, initialBalance.Currency);
            Transactions = new List<WalletTransaction>();

            AddDomainEvent(new WalletCreatedEvent(Id, accountId, initialBalance));
        }

        public void BlockAmount(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Currency != Balance.Currency)
                throw new ArgumentException("Currency mismatch", nameof(amount));
            if (amount.Amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            var oldBlockedAmount = BlockedAmount;
            BlockedAmount += amount;
            Balance -= amount;

            AddDomainEvent(new AmountBlockedEvent(Id, amount, oldBlockedAmount, BlockedAmount));
        }

        public void UnblockAmount(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Currency != Balance.Currency)
                throw new ArgumentException("Currency mismatch", nameof(amount));
            if (amount.Amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));
            if (BlockedAmount < amount)
                throw new InvalidOperationException("Insufficient blocked amount");

            var oldBlockedAmount = BlockedAmount;
            BlockedAmount -= amount;
            Balance += amount;

            AddDomainEvent(new AmountUnblockedEvent(Id, amount, oldBlockedAmount, BlockedAmount));
        }

        public void AddTransaction(WalletTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            if (transaction.Amount.Currency != Balance.Currency)
                throw new ArgumentException("Currency mismatch", nameof(transaction));

            Transactions.Add(transaction);

            switch (transaction.Type)
            {
                case WalletTransactionType.Deposit:
                    Balance += transaction.Amount;
                    break;
                case WalletTransactionType.Withdrawal:
                    if (Balance < transaction.Amount)
                        throw new InvalidOperationException("Insufficient balance");
                    Balance -= transaction.Amount;
                    break;
                case WalletTransactionType.Commission:
                    if (Balance < transaction.Amount)
                        throw new InvalidOperationException("Insufficient balance");
                    Balance -= transaction.Amount;
                    break;
                default:
                    throw new ArgumentException("Invalid transaction type", nameof(transaction));
            }

            AddDomainEvent(new TransactionAddedEvent(Id, transaction));
        }

        public Money GetAvailableBalance()
        {
            return Balance - BlockedAmount;
        }
    }

    public class WalletTransaction
    {
        public Guid Id { get; }
        public WalletTransactionType Type { get; }
        public Money Amount { get; }
        public string Description { get; }
        public DateTime Timestamp { get; }
        public Guid? RelatedOrderId { get; }

        public WalletTransaction(
            WalletTransactionType type,
            Money amount,
            string description,
            Guid? relatedOrderId = null)
        {
            Id = Guid.NewGuid();
            Type = type;
            Amount = amount;
            Description = description;
            Timestamp = DateTime.UtcNow;
            RelatedOrderId = relatedOrderId;
        }
    }

    public enum WalletTransactionType
    {
        Deposit,
        Withdrawal,
        Commission
    }
} 