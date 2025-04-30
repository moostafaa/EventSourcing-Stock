using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreatePortfolioCommand : ICommand
    {
        public Guid AccountId { get; }
        public string Name { get; }
        public string Description { get; }

        public CreatePortfolioCommand(
            Guid accountId,
            string name,
            string description)
        {
            AccountId = accountId;
            Name = name;
            Description = description;
        }
    }

    public class UpdatePortfolioInfoCommand : ICommand
    {
        public Guid PortfolioId { get; }
        public string Name { get; }
        public string Description { get; }

        public UpdatePortfolioInfoCommand(
            Guid portfolioId,
            string name,
            string description)
        {
            PortfolioId = portfolioId;
            Name = name;
            Description = description;
        }
    }

    public class DeletePortfolioCommand : ICommand
    {
        public Guid PortfolioId { get; }
        public string Reason { get; }

        public DeletePortfolioCommand(
            Guid portfolioId,
            string reason)
        {
            PortfolioId = portfolioId;
            Reason = reason;
        }
    }

    public class AddPortfolioItemCommand : ICommand
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }
        public decimal Quantity { get; }

        public AddPortfolioItemCommand(
            Guid portfolioId,
            Guid instrumentId,
            decimal quantity)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
            Quantity = quantity;
        }
    }

    public class UpdatePortfolioItemCommand : ICommand
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }
        public decimal Quantity { get; }

        public UpdatePortfolioItemCommand(
            Guid portfolioId,
            Guid instrumentId,
            decimal quantity)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
            Quantity = quantity;
        }
    }

    public class RemovePortfolioItemCommand : ICommand
    {
        public Guid PortfolioId { get; }
        public Guid InstrumentId { get; }

        public RemovePortfolioItemCommand(
            Guid portfolioId,
            Guid instrumentId)
        {
            PortfolioId = portfolioId;
            InstrumentId = instrumentId;
        }
    }
} 