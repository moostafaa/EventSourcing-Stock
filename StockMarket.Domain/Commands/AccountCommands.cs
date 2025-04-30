using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateAccountCommand : ICommand
    {
        public string Username { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public string? PhoneNumber { get; }
        public string? Address { get; }
        public AccountType Type { get; }

        public CreateAccountCommand(
            string username,
            string email,
            string passwordHash,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            AccountType type,
            string? phoneNumber = null,
            string? address = null)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Type = type;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

    public class UpdateAccountCommand : ICommand
    {
        public Guid AccountId { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? PhoneNumber { get; }
        public string? Address { get; }

        public UpdateAccountCommand(
            Guid accountId,
            string? firstName = null,
            string? lastName = null,
            string? phoneNumber = null,
            string? address = null)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

    public class UpdateAccountBalanceCommand : ICommand
    {
        public Guid AccountId { get; }
        public decimal Amount { get; }
        public BalanceUpdateType UpdateType { get; }

        public UpdateAccountBalanceCommand(
            Guid accountId,
            decimal amount,
            BalanceUpdateType updateType)
        {
            AccountId = accountId;
            Amount = amount;
            UpdateType = updateType;
        }
    }

    public class ChangeAccountPasswordCommand : ICommand
    {
        public Guid AccountId { get; }
        public string CurrentPasswordHash { get; }
        public string NewPasswordHash { get; }

        public ChangeAccountPasswordCommand(
            Guid accountId,
            string currentPasswordHash,
            string newPasswordHash)
        {
            AccountId = accountId;
            CurrentPasswordHash = currentPasswordHash;
            NewPasswordHash = newPasswordHash;
        }
    }

    public class DeactivateAccountCommand : ICommand
    {
        public Guid AccountId { get; }
        public string Reason { get; }

        public DeactivateAccountCommand(Guid accountId, string reason)
        {
            AccountId = accountId;
            Reason = reason;
        }
    }
} 