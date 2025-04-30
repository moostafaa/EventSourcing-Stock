using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Transaction
{
    public class TransactionFailedEvent : DomainEvent
    {
        public Guid TransactionId { get; }
        public string Reason { get; }

        public TransactionFailedEvent(Guid transactionId, string reason)
        {
            TransactionId = transactionId;
            Reason = reason;
        }
    }
} 