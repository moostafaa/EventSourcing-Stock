using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateTradeCommand : ICommand
    {
        public Guid OrderId { get; }
        public Guid InstrumentId { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }
        public DateTime TradeTime { get; }

        public CreateTradeCommand(
            Guid orderId,
            Guid instrumentId,
            decimal quantity,
            decimal price,
            DateTime tradeTime)
        {
            OrderId = orderId;
            InstrumentId = instrumentId;
            Quantity = quantity;
            Price = price;
            TradeTime = tradeTime;
        }
    }

    public class UpdateTradeInfoCommand : ICommand
    {
        public Guid TradeId { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }
        public DateTime TradeTime { get; }

        public UpdateTradeInfoCommand(
            Guid tradeId,
            decimal quantity,
            decimal price,
            DateTime tradeTime)
        {
            TradeId = tradeId;
            Quantity = quantity;
            Price = price;
            TradeTime = tradeTime;
        }
    }

    public class DeleteTradeCommand : ICommand
    {
        public Guid TradeId { get; }
        public string Reason { get; }

        public DeleteTradeCommand(
            Guid tradeId,
            string reason)
        {
            TradeId = tradeId;
            Reason = reason;
        }
    }

    public class ExecuteTradeCommand : ICommand
    {
        public Guid OrderId { get; }
        public Guid CounterpartyOrderId { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }

        public ExecuteTradeCommand(Guid orderId, Guid counterpartyOrderId, decimal quantity, decimal price)
        {
            OrderId = orderId;
            CounterpartyOrderId = counterpartyOrderId;
            Quantity = quantity;
            Price = price;
        }
    }

    public class SettleTradeCommand : ICommand
    {
        public Guid TradeId { get; }
        public SettlementStatus Status { get; }
        public string? SettlementDetails { get; }

        public SettleTradeCommand(Guid tradeId, SettlementStatus status, string? settlementDetails = null)
        {
            TradeId = tradeId;
            Status = status;
            SettlementDetails = settlementDetails;
        }
    }
} 