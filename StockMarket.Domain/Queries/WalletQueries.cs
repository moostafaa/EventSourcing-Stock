using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetWalletByIdQuery : IQuery<Wallet>
    {
        public Guid WalletId { get; }

        public GetWalletByIdQuery(Guid walletId)
        {
            WalletId = walletId;
        }
    }

    public class GetWalletsByAccountIdQuery : IQuery<List<Wallet>>
    {
        public Guid AccountId { get; }

        public GetWalletsByAccountIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetWalletBalanceQuery : IQuery<Money>
    {
        public Guid WalletId { get; }

        public GetWalletBalanceQuery(Guid walletId)
        {
            WalletId = walletId;
        }
    }

    public class GetWalletTransactionsQuery : IQuery<List<WalletTransaction>>
    {
        public Guid WalletId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public WalletTransactionType? Type { get; }

        public GetWalletTransactionsQuery(
            Guid walletId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            WalletTransactionType? type = null)
        {
            WalletId = walletId;
            FromDate = fromDate;
            ToDate = toDate;
            Type = type;
        }
    }

    public class GetWalletBlockedAmountsQuery : IQuery<List<WalletBlockedAmount>>
    {
        public Guid WalletId { get; }

        public GetWalletBlockedAmountsQuery(Guid walletId)
        {
            WalletId = walletId;
        }
    }

    public class GetWalletAvailableBalanceQuery : IQuery<Money>
    {
        public Guid WalletId { get; }

        public GetWalletAvailableBalanceQuery(Guid walletId)
        {
            WalletId = walletId;
        }
    }
} 