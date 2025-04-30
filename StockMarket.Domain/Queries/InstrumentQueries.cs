using System;
using System.Collections.Generic;
using StockMarket.Domain.Common;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Queries
{
    public class GetInstrumentByIdQuery : IQuery<Instrument>
    {
        public Guid InstrumentId { get; }

        public GetInstrumentByIdQuery(Guid instrumentId)
        {
            InstrumentId = instrumentId;
        }
    }

    public class GetInstrumentBySymbolQuery : IQuery<Instrument>
    {
        public string Symbol { get; }

        public GetInstrumentBySymbolQuery(string symbol)
        {
            Symbol = symbol;
        }
    }

    public class GetAllInstrumentsQuery : IQuery<List<Instrument>>
    {
        public InstrumentType? Type { get; }
        public bool? IsActive { get; }

        public GetAllInstrumentsQuery(
            InstrumentType? type = null,
            bool? isActive = null)
        {
            Type = type;
            IsActive = isActive;
        }
    }

    public class GetInstrumentPriceHistoryQuery : IQuery<List<InstrumentPrice>>
    {
        public Guid InstrumentId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
        public TimeFrame TimeFrame { get; }

        public GetInstrumentPriceHistoryQuery(
            Guid instrumentId,
            DateTime fromDate,
            DateTime toDate,
            TimeFrame timeFrame)
        {
            InstrumentId = instrumentId;
            FromDate = fromDate;
            ToDate = toDate;
            TimeFrame = timeFrame;
        }
    }

    public class GetInstrumentVolumeHistoryQuery : IQuery<List<InstrumentVolume>>
    {
        public Guid InstrumentId { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
        public TimeFrame TimeFrame { get; }

        public GetInstrumentVolumeHistoryQuery(
            Guid instrumentId,
            DateTime fromDate,
            DateTime toDate,
            TimeFrame timeFrame)
        {
            InstrumentId = instrumentId;
            FromDate = fromDate;
            ToDate = toDate;
            TimeFrame = timeFrame;
        }
    }

    public class SearchInstrumentsQuery : IQuery<List<Instrument>>
    {
        public string SearchTerm { get; }
        public InstrumentType? Type { get; }
        public bool? IsActive { get; }
        public int? Limit { get; }

        public SearchInstrumentsQuery(
            string searchTerm,
            InstrumentType? type = null,
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