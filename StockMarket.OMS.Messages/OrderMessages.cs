using System;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.OMS.Messages
{
    public class NewOrderMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public Guid AccountId { get; }
        public string Symbol { get; }
        public OrderSide Side { get; }
        public OrderType Type { get; }
        public decimal Quantity { get; }
        public decimal? Price { get; }
        public TimeInForce TimeInForce { get; }
        public string ClientOrderId { get; }

        public NewOrderMessage(
            string exchangeId,
            Guid orderId,
            Guid accountId,
            string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            decimal? price,
            TimeInForce timeInForce,
            string clientOrderId)
            : base(exchangeId, "NewOrder")
        {
            OrderId = orderId;
            AccountId = accountId;
            Symbol = symbol;
            Side = side;
            Type = type;
            Quantity = quantity;
            Price = price;
            TimeInForce = timeInForce;
            ClientOrderId = clientOrderId;
        }
    }

    public class OrderAcceptedMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public string ExchangeOrderId { get; }
        public DateTime AcceptedAt { get; }

        public OrderAcceptedMessage(
            string exchangeId,
            Guid orderId,
            string exchangeOrderId)
            : base(exchangeId, "OrderAccepted")
        {
            OrderId = orderId;
            ExchangeOrderId = exchangeOrderId;
            AcceptedAt = DateTime.UtcNow;
        }
    }

    public class OrderRejectedMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public string RejectReason { get; }
        public string RejectCode { get; }

        public OrderRejectedMessage(
            string exchangeId,
            Guid orderId,
            string rejectReason,
            string rejectCode)
            : base(exchangeId, "OrderRejected")
        {
            OrderId = orderId;
            RejectReason = rejectReason;
            RejectCode = rejectCode;
        }
    }

    public class OrderCancelledMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public string ExchangeOrderId { get; }
        public decimal CancelledQuantity { get; }
        public string CancelReason { get; }

        public OrderCancelledMessage(
            string exchangeId,
            Guid orderId,
            string exchangeOrderId,
            decimal cancelledQuantity,
            string cancelReason)
            : base(exchangeId, "OrderCancelled")
        {
            OrderId = orderId;
            ExchangeOrderId = exchangeOrderId;
            CancelledQuantity = cancelledQuantity;
            CancelReason = cancelReason;
        }
    }

    public class OrderFilledMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public string ExchangeOrderId { get; }
        public decimal FilledQuantity { get; }
        public decimal FillPrice { get; }
        public decimal Commission { get; }
        public string Currency { get; }

        public OrderFilledMessage(
            string exchangeId,
            Guid orderId,
            string exchangeOrderId,
            decimal filledQuantity,
            decimal fillPrice,
            decimal commission,
            string currency)
            : base(exchangeId, "OrderFilled")
        {
            OrderId = orderId;
            ExchangeOrderId = exchangeOrderId;
            FilledQuantity = filledQuantity;
            FillPrice = fillPrice;
            Commission = commission;
            Currency = currency;
        }
    }

    public class OrderStatusMessage : BaseMessage
    {
        public Guid OrderId { get; }
        public string ExchangeOrderId { get; }
        public OrderStatus Status { get; }
        public decimal FilledQuantity { get; }
        public decimal RemainingQuantity { get; }
        public decimal AveragePrice { get; }

        public OrderStatusMessage(
            string exchangeId,
            Guid orderId,
            string exchangeOrderId,
            OrderStatus status,
            decimal filledQuantity,
            decimal remainingQuantity,
            decimal averagePrice)
            : base(exchangeId, "OrderStatus")
        {
            OrderId = orderId;
            ExchangeOrderId = exchangeOrderId;
            Status = status;
            FilledQuantity = filledQuantity;
            RemainingQuantity = remainingQuantity;
            AveragePrice = averagePrice;
        }
    }
} 