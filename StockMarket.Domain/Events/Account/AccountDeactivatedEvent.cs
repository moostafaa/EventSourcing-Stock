using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Account
{
    public class AccountDeactivatedEvent : DomainEvent
    {
        public Guid AccountId { get; }

        public AccountDeactivatedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
} 