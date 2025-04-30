using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Account
{
    public class AccountInfoUpdatedEvent : DomainEvent
    {
        public Guid AccountId { get; }

        public AccountInfoUpdatedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
} 