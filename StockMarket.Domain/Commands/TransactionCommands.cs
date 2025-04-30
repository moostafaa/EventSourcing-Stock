using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateTransactionCommand : ICommand
    {
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public TransactionType Type { get; }
        public decimal Amount { get; }
        public string Description { get; }
        public DateTime TransactionTime { get; }

        public CreateTransactionCommand(
            Guid accountId,
            Guid instrumentId,
            TransactionType type,
            decimal amount,
            string description,
            DateTime transactionTime)
        {
            AccountId = accountId;
            InstrumentId = instrumentId;
            Type = type;
            Amount = amount;
            Description = description;
            TransactionTime = transactionTime;
        }
    }

    public class UpdateTransactionInfoCommand : ICommand
    {
        public Guid TransactionId { get; }
        public decimal Amount { get; }
        public string Description { get; }
        public DateTime TransactionTime { get; }

        public UpdateTransactionInfoCommand(
            Guid transactionId,
            decimal amount,
            string description,
            DateTime transactionTime)
        {
            TransactionId = transactionId;
            Amount = amount;
            Description = description;
            TransactionTime = transactionTime;
        }
    }

    public class DeleteTransactionCommand : ICommand
    {
        public Guid TransactionId { get; }
        public string Reason { get; }

        public DeleteTransactionCommand(
            Guid transactionId,
            string reason)
        {
            TransactionId = transactionId;
            Reason = reason;
        }
    }

    public class SettleTransactionCommand : ICommand
    {
        public Guid TransactionId { get; }
        public SettlementStatus Status { get; }
        public string? SettlementDetails { get; }

        public SettleTransactionCommand(Guid transactionId, SettlementStatus status, string? settlementDetails = null)
        {
            TransactionId = transactionId;
            Status = status;
            SettlementDetails = settlementDetails;
        }
    }
} 