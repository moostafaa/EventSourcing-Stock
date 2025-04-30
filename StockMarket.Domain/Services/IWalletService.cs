using System;
using System.Threading.Tasks;

namespace StockMarket.Domain.Services
{
    public interface IWalletService
    {
        Task CreateWalletAsync(Guid accountId);
        Task AddAmountAsync(Guid accountId, decimal amount);
        Task DeductAmountAsync(Guid accountId, decimal amount);
        Task BlockAmountAsync(Guid accountId, decimal amount);
        Task ReleaseBlockedAmountAsync(Guid accountId, decimal amount);
        Task UpdateBlockedAmountAsync(Guid accountId, decimal newAmount);
        Task<decimal> GetAvailableBalanceAsync(Guid accountId);
    }
} 