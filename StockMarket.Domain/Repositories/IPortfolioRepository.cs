using System;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetByAccountIdAsync(Guid accountId);
        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
    }
} 