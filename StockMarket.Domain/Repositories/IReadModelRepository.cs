using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarket.Domain.Repositories
{
    public interface IReadModelRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
} 