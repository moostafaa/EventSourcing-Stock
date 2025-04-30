using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Account
{
    public class MoneyReleasedEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Guid OrderId { get; }
        public Money Amount { get; }

        public MoneyReleasedEvent(Guid accountId, Guid orderId, Money amount)
        {
            AccountId = accountId;
            OrderId = orderId;
            Amount = amount;
        }
    }
} 