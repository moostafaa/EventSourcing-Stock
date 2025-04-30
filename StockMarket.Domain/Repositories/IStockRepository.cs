using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface IStockRepository
    {
        Task<Stock> GetByIdAsync(Guid id);
        Task<Stock> GetBySymbolAsync(string symbol);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(Guid id);
    }
} 