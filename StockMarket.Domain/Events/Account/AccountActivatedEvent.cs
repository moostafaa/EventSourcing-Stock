using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Account
{
    public class AccountActivatedEvent : DomainEvent
    {
        public Guid AccountId { get; }

        public AccountActivatedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
} 