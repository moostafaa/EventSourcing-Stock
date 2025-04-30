using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetAccountByIdQuery : IQuery<Account>
    {
        public Guid AccountId { get; }

        public GetAccountByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetAccountByUsernameQuery : IQuery<Account>
    {
        public string Username { get; }

        public GetAccountByUsernameQuery(string username)
        {
            Username = username;
        }
    }

    public class GetAccountByEmailQuery : IQuery<Account>
    {
        public string Email { get; }

        public GetAccountByEmailQuery(string email)
        {
            Email = email;
        }
    }

    public class GetAllAccountsQuery : IQuery<List<Account>>
    {
        public AccountType? Type { get; }
        public bool? IsActive { get; }

        public GetAllAccountsQuery(
            AccountType? type = null,
            bool? isActive = null)
        {
            Type = type;
            IsActive = isActive;
        }
    }

    public class GetAccountBalanceQuery : IQuery<Money>
    {
        public Guid AccountId { get; }

        public GetAccountBalanceQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetAccountPortfoliosQuery : IQuery<List<Portfolio>>
    {
        public Guid AccountId { get; }

        public GetAccountPortfoliosQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetAccountOrdersQuery : IQuery<List<Order>>
    {
        public Guid AccountId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public OrderStatus? Status { get; }

        public GetAccountOrdersQuery(
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

    public class GetAccountTradesQuery : IQuery<List<Trade>>
    {
        public Guid AccountId { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }

        public GetAccountTradesQuery(
            Guid accountId,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            AccountId = accountId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }

    public class SearchAccountsQuery : IQuery<List<Account>>
    {
        public string SearchTerm { get; }
        public AccountType? Type { get; }
        public bool? IsActive { get; }
        public int? Limit { get; }

        public SearchAccountsQuery(
            string searchTerm,
            AccountType? type = null,
            bool? isActive = null,
            int? limit = null)
        {
            SearchTerm = searchTerm;
            Type = type;
            IsActive = isActive;
            Limit = limit;
        }
    }
} 