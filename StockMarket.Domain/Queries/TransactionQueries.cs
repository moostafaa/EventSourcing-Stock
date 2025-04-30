using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetTransactionByIdQuery : IQuery<Transaction>
    {
        public Guid TransactionId { get; }

        public GetTransactionByIdQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
    }

    public class GetTransactionsByAccountIdQuery : IQuery<List<Transaction>>
    {
        public Guid AccountId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public TransactionType? Type { get; }

        public GetTransactionsByAccountIdQuery(
            Guid accountId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            TransactionType? type = null)
        {
            AccountId = accountId;
            FromDate = fromDate;
            ToDate = toDate;
            Type = type;
        }
    }

    public class GetTransactionsByInstrumentIdQuery : IQuery<List<Transaction>>
    {
        public Guid InstrumentId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public TransactionType? Type { get; }

        public GetTransactionsByInstrumentIdQuery(
            Guid instrumentId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            TransactionType? type = null)
        {
            InstrumentId = instrumentId;
            FromDate = fromDate;
            ToDate = toDate;
            Type = type;
        }
    }

    public class GetTransactionHistoryQuery : IQuery<List<Transaction>>
    {
        public Guid AccountId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
        public TransactionType? Type { get; }
        public int? Limit { get; }

        public GetTransactionHistoryQuery(
            Guid accountId,
            DateTime fromDate,
            DateTime toDate,
            TransactionType? type = null,
            int? limit = null)
        {
            AccountId = accountId;
            FromDate = fromDate;
            ToDate = toDate;
            Type = type;
            Limit = limit;
        }
    }
} 