using System;

namespace StockMarket.OMS.Messages
{
    public class AccountBalanceMessage : BaseMessage
    {
        public Guid AccountId { get; }
        public string Currency { get; }
        public decimal AvailableBalance { get; }
        public decimal BlockedBalance { get; }
        public decimal TotalBalance { get; }
        public DateTime Timestamp { get; }

        public AccountBalanceMessage(
            string exchangeId,
            Guid accountId,
            string currency,
            decimal availableBalance,
            decimal blockedBalance)
            : base(exchangeId, "AccountBalance")
        {
            AccountId = accountId;
            Currency = currency;
            AvailableBalance = availableBalance;
            BlockedBalance = blockedBalance;
            TotalBalance = availableBalance + blockedBalance;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class PositionMessage : BaseMessage
    {
        public Guid AccountId { get; }
        public string Symbol { get; }
        public decimal Quantity { get; }
        public decimal AveragePrice { get; }
        public decimal UnrealizedPnL { get; }
        public decimal RealizedPnL { get; }
        public DateTime Timestamp { get; }

        public PositionMessage(
            string exchangeId,
            Guid accountId,
            string symbol,
            decimal quantity,
            decimal averagePrice,
            decimal unrealizedPnL,
            decimal realizedPnL)
            : base(exchangeId, "Position")
        {
            AccountId = accountId;
            Symbol = symbol;
            Quantity = quantity;
            AveragePrice = averagePrice;
            UnrealizedPnL = unrealizedPnL;
            RealizedPnL = realizedPnL;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class MarginCallMessage : BaseMessage
    {
        public Guid AccountId { get; }
        public string Currency { get; }
        public decimal RequiredMargin { get; }
        public decimal AvailableMargin { get; }
        public decimal MarginDeficit { get; }
        public DateTime Timestamp { get; }

        public MarginCallMessage(
            string exchangeId,
            Guid accountId,
            string currency,
            decimal requiredMargin,
            decimal availableMargin)
            : base(exchangeId, "MarginCall")
        {
            AccountId = accountId;
            Currency = currency;
            RequiredMargin = requiredMargin;
            AvailableMargin = availableMargin;
            MarginDeficit = requiredMargin - availableMargin;
            Timestamp = DateTime.UtcNow;
        }
    }
} 