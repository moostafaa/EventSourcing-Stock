using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Transaction;

namespace StockMarket.Domain.Entities
{
    public class Transaction : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public string ReferenceNumber { get; private set; }

        // Trade-related properties
        public Guid? TradeId { get; private set; }
        public Guid? InstrumentId { get; private set; }
        public int? Quantity { get; private set; }
        public decimal? Price { get; private set; }

        private Transaction() { }

        public Transaction(
            Guid accountId,
            TransactionType type,
            decimal amount,
            string description,
            string referenceNumber)
        {
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Description = description;
            Status = TransactionStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            ReferenceNumber = referenceNumber;

            AddDomainEvent(new TransactionCreatedEvent(Id, accountId, type, amount, description));
        }

        public Transaction(
            Guid accountId,
            TransactionType type,
            decimal amount,
            string description,
            string referenceNumber,
            Guid tradeId,
            Guid instrumentId,
            int quantity,
            decimal price)
            : this(accountId, type, amount, description, referenceNumber)
        {
            TradeId = tradeId;
            InstrumentId = instrumentId;
            Quantity = quantity;
            Price = price;
        }

        public void Complete()
        {
            if (Status != TransactionStatus.Pending)
                throw new InvalidOperationException("Transaction is not in pending status");

            Status = TransactionStatus.Completed;
            CompletedAt = DateTime.UtcNow;

            AddDomainEvent(new TransactionCompletedEvent(Id));
        }

        public void Fail(string reason)
        {
            if (Status != TransactionStatus.Pending)
                throw new InvalidOperationException("Transaction is not in pending status");

            Status = TransactionStatus.Failed;
            CompletedAt = DateTime.UtcNow;
            Description += $" - Failed: {reason}";

            AddDomainEvent(new TransactionFailedEvent(Id, reason));
        }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        TradeBuy,
        TradeSell,
        Fee,
        Interest,
        Dividend
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Cancelled
    }
} 