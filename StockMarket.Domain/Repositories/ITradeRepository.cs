using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface ITradeRepository
    {
        Task<Trade> GetByIdAsync(Guid id);
        Task<IEnumerable<Trade>> GetByAccountIdAsync(Guid accountId);
        Task<IEnumerable<Trade>> GetByInstrumentIdAsync(Guid instrumentId);
        Task<IEnumerable<Trade>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(Trade trade);
    }
} 