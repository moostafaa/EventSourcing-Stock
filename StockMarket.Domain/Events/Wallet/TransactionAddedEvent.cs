using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Wallet
{
    public class TransactionAddedEvent : DomainEvent
    {
        public WalletTransaction Transaction { get; }

        public TransactionAddedEvent(
            Guid walletId,
            WalletTransaction transaction)
            : base(walletId)
        {
            Transaction = transaction;
        }
    }
} 