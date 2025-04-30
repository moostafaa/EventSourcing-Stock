using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Events.Wallet
{
    public class AmountUnblockedEvent : DomainEvent
    {
        public Money Amount { get; }
        public Money OldBlockedAmount { get; }
        public Money NewBlockedAmount { get; }

        public AmountUnblockedEvent(
            Guid walletId,
            Money amount,
            Money oldBlockedAmount,
            Money newBlockedAmount)
            : base(walletId)
        {
            Amount = amount;
            OldBlockedAmount = oldBlockedAmount;
            NewBlockedAmount = newBlockedAmount;
        }
    }
} 