using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByAccountIdAsync(Guid accountId);
        Task<IEnumerable<Order>> GetByInstrumentIdAsync(Guid instrumentId);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
} 