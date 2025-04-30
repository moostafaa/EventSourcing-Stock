using System;
using StockMarket.OMS.Messages.Common;
using StockMarket.Domain.Enums;

namespace StockMarket.OMS.Messages.Orders
{
    public class CreateOrderMessage : OrderMessage
    {
        public CreateOrderMessage(
            Guid orderId,
            Guid accountId,
            Guid instrumentId,
            decimal quantity,
            decimal price,
            string currency,
            OrderType orderType,
            OrderSide orderSide,
            TimeInForce timeInForce)
            : base("CreateOrder", orderId, accountId, instrumentId, quantity, price, currency, orderType, orderSide, timeInForce)
        {
        }
    }
} 