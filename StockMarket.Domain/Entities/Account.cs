using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Account;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Entities
{
    public class Account : AggregateRoot
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public AccountType Type { get; private set; }
        public AccountStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public Money Balance { get; private set; }
        public Money BlockedAmount { get; private set; }

        // Individual account properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalId { get; private set; }

        // Legal entity properties
        public string CompanyName { get; private set; }
        public string RegistrationNumber { get; private set; }
        public string TaxId { get; private set; }

        private readonly List<OrderBlock> _orderBlocks = new List<OrderBlock>();
        public IReadOnlyCollection<OrderBlock> OrderBlocks => _orderBlocks.AsReadOnly();

        private Account() { }

        public Account(string username, string email, string passwordHash, AccountType type)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Type = type;
            Status = AccountStatus.Active;
            CreatedAt = DateTime.UtcNow;

            AddDomainEvent(new AccountCreatedEvent(Id, username, email, type));
        }

        public void UpdateIndividualInfo(string firstName, string lastName, string nationalId)
        {
            if (Type != AccountType.Individual)
                throw new InvalidOperationException("Cannot set individual information for a legal entity account");

            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;

            AddDomainEvent(new AccountInfoUpdatedEvent(Id));
        }

        public void UpdateLegalInfo(string companyName, string registrationNumber, string taxId)
        {
            if (Type != AccountType.Legal)
                throw new InvalidOperationException("Cannot set legal entity information for an individual account");

            CompanyName = companyName;
            RegistrationNumber = registrationNumber;
            TaxId = taxId;

            AddDomainEvent(new AccountInfoUpdatedEvent(Id));
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Status = AccountStatus.Inactive;
            AddDomainEvent(new AccountDeactivatedEvent(Id));
        }

        public void Activate()
        {
            Status = AccountStatus.Active;
            AddDomainEvent(new AccountActivatedEvent(Id));
        }

        public void BlockMoneyForOrder(Guid orderId, Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Currency mismatch");
            if (amount > (Balance - BlockedAmount))
                throw new InvalidOperationException("Insufficient available balance");

            var block = new OrderBlock(orderId, amount);
            _orderBlocks.Add(block);
            BlockedAmount += amount;

            AddDomainEvent(new MoneyBlockedEvent(Id, orderId, amount));
        }

        public void ReleaseBlockedMoney(Guid orderId)
        {
            var block = _orderBlocks.FirstOrDefault(b => b.OrderId == orderId);
            if (block == null)
                throw new InvalidOperationException("No money blocked for this order");

            _orderBlocks.Remove(block);
            BlockedAmount -= block.Amount;

            AddDomainEvent(new MoneyReleasedEvent(Id, orderId, block.Amount));
        }

        public void UpdateBlockedMoney(Guid orderId, Money newAmount)
        {
            if (newAmount == null)
                throw new ArgumentNullException(nameof(newAmount));
            if (newAmount.Currency != Balance.Currency)
                throw new InvalidOperationException("Currency mismatch");

            var block = _orderBlocks.FirstOrDefault(b => b.OrderId == orderId);
            if (block == null)
                throw new InvalidOperationException("No money blocked for this order");

            var difference = newAmount - block.Amount;
            if (difference > (Balance - BlockedAmount))
                throw new InvalidOperationException("Insufficient available balance");

            block.UpdateAmount(newAmount);
            BlockedAmount += difference;

            AddDomainEvent(new BlockedMoneyUpdatedEvent(Id, orderId, block.Amount, newAmount));
        }

        public void Deposit(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Currency mismatch");

            Balance += amount;
            AddDomainEvent(new MoneyDepositedEvent(Id, amount));
        }

        public void Withdraw(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Currency != Balance.Currency)
                throw new InvalidOperationException("Currency mismatch");
            if (amount > (Balance - BlockedAmount))
                throw new InvalidOperationException("Insufficient available balance");

            Balance -= amount;
            AddDomainEvent(new MoneyWithdrawnEvent(Id, amount));
        }
    }

    public enum AccountType
    {
        Individual,
        Legal
    }

    public enum AccountStatus
    {
        Active,
        Inactive,
        Suspended
    }

    public class OrderBlock
    {
        public Guid OrderId { get; }
        public Money Amount { get; private set; }

        public OrderBlock(Guid orderId, Money amount)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("Order ID cannot be empty", nameof(orderId));
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            OrderId = orderId;
            Amount = amount;
        }

        public void UpdateAmount(Money newAmount)
        {
            if (newAmount == null)
                throw new ArgumentNullException(nameof(newAmount));
            if (newAmount.Currency != Amount.Currency)
                throw new InvalidOperationException("Currency mismatch");

            Amount = newAmount;
        }
    }
} 