using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Transaction
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public Guid AccountId { get; }
        public TransactionType Type { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public TransactionCreatedEvent(Guid transactionId, Guid accountId, TransactionType type, decimal amount, string description)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Description = description;
        }
    }
} 