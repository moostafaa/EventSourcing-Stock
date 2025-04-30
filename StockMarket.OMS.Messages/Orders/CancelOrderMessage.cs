using System;
using StockMarket.OMS.Messages.Common;

namespace StockMarket.OMS.Messages.Orders
{
    public class CancelOrderMessage : OrderMessage
    {
        public CancelOrderMessage(
            Guid orderId,
            Guid accountId,
            Guid instrumentId)
            : base("CancelOrder", orderId, accountId, instrumentId, 0, 0, string.Empty, OrderType.Limit, OrderSide.Buy, TimeInForce.GTC)
        {
        }
    }
} 