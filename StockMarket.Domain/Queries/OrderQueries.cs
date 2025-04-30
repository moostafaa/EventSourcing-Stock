using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetOrderByIdQuery : IQuery<Order>
    {
        public Guid OrderId { get; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }

    public class GetOrdersByAccountIdQuery : IQuery<List<Order>>
    {
        public Guid AccountId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public OrderStatus? Status { get; }

        public GetOrdersByAccountIdQuery(
            Guid accountId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            OrderStatus? status = null)
        {
            AccountId = accountId;
            FromDate = fromDate;
            ToDate = toDate;
            Status = status;
        }
    }

    public class GetOrdersByInstrumentIdQuery : IQuery<List<Order>>
    {
        public Guid InstrumentId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public OrderStatus? Status { get; }

        public GetOrdersByInstrumentIdQuery(
            Guid instrumentId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            OrderStatus? status = null)
        {
            InstrumentId = instrumentId;
            FromDate = fromDate;
            ToDate = toDate;
            Status = status;
        }
    }

    public class GetActiveOrdersQuery : IQuery<List<Order>>
    {
        public Guid? AccountId { get; }
        public Guid? InstrumentId { get; }
        public OrderSide? Side { get; }

        public GetActiveOrdersQuery(
            Guid? accountId = null,
            Guid? instrumentId = null,
            OrderSide? side = null)
        {
            AccountId = accountId;
            InstrumentId = instrumentId;
            Side = side;
        }
    }

    public class GetOrderBookQuery : IQuery<OrderBook>
    {
        public Guid InstrumentId { get; }

        public GetOrderBookQuery(Guid instrumentId)
        {
            InstrumentId = instrumentId;
        }
    }
} 