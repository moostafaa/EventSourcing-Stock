using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Wallet
{
    public class WalletCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; }
        public Money InitialBalance { get; }

        public WalletCreatedEvent(
            Guid walletId,
            Guid accountId,
            Money initialBalance)
            : base(walletId)
        {
            AccountId = accountId;
            InitialBalance = initialBalance;
        }
    }
} 