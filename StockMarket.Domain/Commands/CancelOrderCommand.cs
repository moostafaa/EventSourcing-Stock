using System;

namespace StockMarket.Domain.Commands
{
    public class CancelOrderCommand
    {
        public Guid OrderId { get; }
        public string Reason { get; }

        public CancelOrderCommand(
            Guid orderId,
            string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
} 