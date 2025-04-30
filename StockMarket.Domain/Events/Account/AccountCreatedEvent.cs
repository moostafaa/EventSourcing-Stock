using StockMarket.Domain.Common;

namespace StockMarket.Domain.Events.Account
{
    public class AccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public string Username { get; }
        public string Email { get; }
        public AccountType Type { get; }

        public AccountCreatedEvent(Guid accountId, string username, string email, AccountType type)
        {
            AccountId = accountId;
            Username = username;
            Email = email;
            Type = type;
        }
    }
} 