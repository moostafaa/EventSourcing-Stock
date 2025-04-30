using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateWalletCommand : ICommand
    {
        public Guid AccountId { get; }
        public decimal InitialBalance { get; }
        public string Currency { get; }

        public CreateWalletCommand(
            Guid accountId,
            decimal initialBalance,
            string currency)
        {
            AccountId = accountId;
            InitialBalance = initialBalance;
            Currency = currency;
        }
    }

    public class DepositToWalletCommand : ICommand
    {
        public Guid WalletId { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public DepositToWalletCommand(
            Guid walletId,
            decimal amount,
            string description)
        {
            WalletId = walletId;
            Amount = amount;
            Description = description;
        }
    }

    public class WithdrawFromWalletCommand : ICommand
    {
        public Guid WalletId { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public WithdrawFromWalletCommand(
            Guid walletId,
            decimal amount,
            string description)
        {
            WalletId = walletId;
            Amount = amount;
            Description = description;
        }
    }

    public class BlockWalletAmountCommand : ICommand
    {
        public Guid WalletId { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public BlockWalletAmountCommand(
            Guid walletId,
            decimal amount,
            string description)
        {
            WalletId = walletId;
            Amount = amount;
            Description = description;
        }
    }

    public class UnblockWalletAmountCommand : ICommand
    {
        public Guid WalletId { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public UnblockWalletAmountCommand(
            Guid walletId,
            decimal amount,
            string description)
        {
            WalletId = walletId;
            Amount = amount;
            Description = description;
        }
    }

    public class DeleteWalletCommand : ICommand
    {
        public Guid WalletId { get; }
        public string Reason { get; }

        public DeleteWalletCommand(
            Guid walletId,
            string reason)
        {
            WalletId = walletId;
            Reason = reason;
        }
    }
} 