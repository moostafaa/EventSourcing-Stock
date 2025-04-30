using System;
using StockMarket.Domain.Common;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class CreateInstrumentCommand : ICommand
    {
        public string Symbol { get; }
        public string Name { get; }
        public string Description { get; }
        public InstrumentType Type { get; }
        public decimal InitialPrice { get; }
        public decimal InitialVolume { get; }

        public CreateInstrumentCommand(
            string symbol,
            string name,
            string description,
            InstrumentType type,
            decimal initialPrice,
            decimal initialVolume)
        {
            Symbol = symbol;
            Name = name;
            Description = description;
            Type = type;
            InitialPrice = initialPrice;
            InitialVolume = initialVolume;
        }
    }

    public class UpdateInstrumentInfoCommand : ICommand
    {
        public Guid InstrumentId { get; }
        public string? Name { get; }
        public string? Description { get; }

        public UpdateInstrumentInfoCommand(
            Guid instrumentId,
            string? name = null,
            string? description = null)
        {
            InstrumentId = instrumentId;
            Name = name;
            Description = description;
        }
    }

    public class UpdateInstrumentPriceCommand : ICommand
    {
        public Guid InstrumentId { get; }
        public decimal NewPrice { get; }

        public UpdateInstrumentPriceCommand(
            Guid instrumentId,
            decimal newPrice)
        {
            InstrumentId = instrumentId;
            NewPrice = newPrice;
        }
    }

    public class UpdateInstrumentVolumeCommand : ICommand
    {
        public Guid InstrumentId { get; }
        public decimal NewVolume { get; }

        public UpdateInstrumentVolumeCommand(
            Guid instrumentId,
            decimal newVolume)
        {
            InstrumentId = instrumentId;
            NewVolume = newVolume;
        }
    }

    public class DeactivateInstrumentCommand : ICommand
    {
        public Guid InstrumentId { get; }
        public string Reason { get; }

        public DeactivateInstrumentCommand(
            Guid instrumentId,
            string reason)
        {
            InstrumentId = instrumentId;
            Reason = reason;
        }
    }
} 