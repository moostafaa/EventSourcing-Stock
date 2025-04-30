using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Domain.ValueObjects;
using StockMarket.Domain.Repositories;

namespace StockMarket.Domain.ReadModels
{
    public class WalletReadModel
    {
        private readonly IReadModelRepository<WalletEntry> _repository;

        public WalletReadModel(IReadModelRepository<WalletEntry> repository)
        {
            _repository = repository;
        }

        public async Task AddWallet(Guid walletId, Guid accountId)
        {
            var entry = new WalletEntry
            {
                WalletId = walletId,
                AccountId = accountId,
                Balance = 0,
                BlockedAmount = 0,
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveAsync(entry);
        }

        public async Task AddAmount(Guid walletId, decimal amount)
        {
            var entry = await _repository.GetByIdAsync(walletId);
            if (entry != null)
            {
                entry.Balance += amount;
                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task DeductAmount(Guid walletId, decimal amount)
        {
            var entry = await _repository.GetByIdAsync(walletId);
            if (entry != null)
            {
                entry.Balance -= amount;
                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task BlockAmount(Guid walletId, decimal amount)
        {
            var entry = await _repository.GetByIdAsync(walletId);
            if (entry != null)
            {
                entry.BlockedAmount += amount;
                entry.Balance -= amount;
                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task UnblockAmount(Guid walletId, decimal amount)
        {
            var entry = await _repository.GetByIdAsync(walletId);
            if (entry != null)
            {
                entry.BlockedAmount -= amount;
                entry.Balance += amount;
                entry.Timestamp = DateTime.UtcNow;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task<WalletEntry> GetWallet(Guid walletId)
        {
            return await _repository.GetByIdAsync(walletId);
        }

        public async Task<IEnumerable<WalletEntry>> GetWalletsByAccount(Guid accountId)
        {
            var allEntries = await _repository.GetAllAsync();
            return allEntries.Where(e => e.AccountId == accountId);
        }

        public Guid AccountId { get; }
        public Money Balance { get; private set; }
        public Money BlockedAmount { get; private set; }
        public List<WalletTransaction> Transactions { get; }
        public Money AvailableBalance => Balance - BlockedAmount;

        public WalletReadModel(Guid accountId)
        {
            AccountId = accountId;
            Balance = new Money(0, "USD"); // Default currency
            BlockedAmount = new Money(0, "USD");
            Transactions = new List<WalletTransaction>();
        }

        public void ApplyWalletCreated(Money initialBalance)
        {
            Balance = initialBalance;
        }

        public void ApplyAmountBlocked(Money amount)
        {
            BlockedAmount += amount;
            Balance -= amount;
        }

        public void ApplyAmountUnblocked(Money amount)
        {
            BlockedAmount -= amount;
            Balance += amount;
        }

        public void ApplyTransactionAdded(WalletTransaction transaction)
        {
            Transactions.Add(transaction);

            switch (transaction.Type)
            {
                case WalletTransactionType.Deposit:
                    Balance += transaction.Amount;
                    break;
                case WalletTransactionType.Withdrawal:
                case WalletTransactionType.Commission:
                    Balance -= transaction.Amount;
                    break;
            }
        }

        public decimal GetTotalDeposits()
        {
            return Transactions
                .Where(t => t.Type == WalletTransactionType.Deposit)
                .Sum(t => t.Amount.Amount);
        }

        public decimal GetTotalWithdrawals()
        {
            return Transactions
                .Where(t => t.Type == WalletTransactionType.Withdrawal)
                .Sum(t => t.Amount.Amount);
        }

        public decimal GetTotalCommissions()
        {
            return Transactions
                .Where(t => t.Type == WalletTransactionType.Commission)
                .Sum(t => t.Amount.Amount);
        }

        public List<WalletTransaction> GetRecentTransactions(int count = 10)
        {
            return Transactions
                .OrderByDescending(t => t.Timestamp)
                .Take(count)
                .ToList();
        }
    }

    public class WalletEntry
    {
        public Guid WalletId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public decimal BlockedAmount { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 