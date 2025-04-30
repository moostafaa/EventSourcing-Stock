using System;
using StockMarket.OMS.Messages.Common;

namespace StockMarket.OMS.Messages.Orders
{
    public class EditOrderMessage : OrderMessage
    {
        public EditOrderMessage(
            Guid orderId,
            Guid accountId,
            Guid instrumentId,
            decimal quantity,
            decimal price,
            string currency)
            : base(orderId, accountId, instrumentId, quantity, price, currency, OrderType.Limit, OrderSide.Buy, TimeInForce.GTC)
        {
        }
    }
} 