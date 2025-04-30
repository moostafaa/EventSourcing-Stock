using System;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> GetByUsernameAsync(string username);
        Task<Account> GetByEmailAsync(string email);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
    }
} 