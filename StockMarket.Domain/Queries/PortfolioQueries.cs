using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetPortfolioByIdQuery : IQuery<Portfolio>
    {
        public Guid PortfolioId { get; }

        public GetPortfolioByIdQuery(Guid portfolioId)
        {
            PortfolioId = portfolioId;
        }
    }

    public class GetPortfoliosByAccountIdQuery : IQuery<List<Portfolio>>
    {
        public Guid AccountId { get; }

        public GetPortfoliosByAccountIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetPortfolioItemsQuery : IQuery<List<PortfolioItem>>
    {
        public Guid PortfolioId { get; }

        public GetPortfolioItemsQuery(Guid portfolioId)
        {
            PortfolioId = portfolioId;
        }
    }

    public class GetPortfolioItemByInstrumentIdQuery : IQuery<PortfolioItem>
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }

        public GetPortfolioItemByInstrumentIdQuery(Guid portfolioId, Guid instrumentId)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
        }
    }

    public class GetPortfolioValueQuery : IQuery<Money>
    {
        public Guid PortfolioId { get; }
        public DateTime? AsOfDate { get; }

        public GetPortfolioValueQuery(Guid portfolioId, DateTime? asOfDate = null)
        {
            PortfolioId = portfolioId;
            AsOfDate = asOfDate;
        }
    }

    public class GetPortfolioPerformanceQuery : IQuery<PortfolioPerformance>
    {
        public Guid PortfolioId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public GetPortfolioPerformanceQuery(
            Guid portfolioId,
            DateTime fromDate,
            DateTime toDate)
        {
            PortfolioId = portfolioId;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
} 