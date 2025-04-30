using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Account
{
    public class BlockedMoneyUpdatedEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Guid OrderId { get; }
        public Money OldAmount { get; }
        public Money NewAmount { get; }

        public BlockedMoneyUpdatedEvent(Guid accountId, Guid orderId, Money oldAmount, Money newAmount)
        {
            AccountId = accountId;
            OrderId = orderId;
            OldAmount = oldAmount;
            NewAmount = newAmount;
        }
    }
} 