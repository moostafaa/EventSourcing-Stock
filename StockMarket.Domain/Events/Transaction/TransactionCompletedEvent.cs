using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Transaction
{
    public class TransactionCompletedEvent : DomainEvent
    {
        public Guid TransactionId { get; }

        public TransactionCompletedEvent(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }
} 