using StockMarket.Domain.Common;
using StockMarket.Domain.Events;
using StockMarket.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace StockMarket.Domain.EventHandlers
{
    public class InstrumentCreatedEventHandler : IEventHandler<InstrumentCreatedEvent>
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly ILogger<InstrumentCreatedEventHandler> _logger;

        public InstrumentCreatedEventHandler(IInstrumentRepository instrumentRepository, ILogger<InstrumentCreatedEventHandler> logger)
        {
            _instrumentRepository = instrumentRepository;
            _logger = logger;
        }

        public async Task Handle(InstrumentCreatedEvent @event)
        {
            _logger.LogInformation($"Handling InstrumentCreatedEvent for instrument {@event.Symbol}");
            // Additional handling logic can be added here
        }
    }

    public class InstrumentInfoUpdatedEventHandler : IEventHandler<InstrumentInfoUpdatedEvent>
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly ILogger<InstrumentInfoUpdatedEventHandler> _logger;

        public InstrumentInfoUpdatedEventHandler(IInstrumentRepository instrumentRepository, ILogger<InstrumentInfoUpdatedEventHandler> logger)
        {
            _instrumentRepository = instrumentRepository;
            _logger = logger;
        }

        public async Task Handle(InstrumentInfoUpdatedEvent @event)
        {
            _logger.LogInformation($"Handling InstrumentInfoUpdatedEvent for instrument {@event.InstrumentId}");
            // Additional handling logic can be added here
        }
    }

    public class InstrumentPriceUpdatedEventHandler : IEventHandler<InstrumentPriceUpdatedEvent>
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly ILogger<InstrumentPriceUpdatedEventHandler> _logger;

        public InstrumentPriceUpdatedEventHandler(IInstrumentRepository instrumentRepository, ILogger<InstrumentPriceUpdatedEventHandler> logger)
        {
            _instrumentRepository = instrumentRepository;
            _logger = logger;
        }

        public async Task Handle(InstrumentPriceUpdatedEvent @event)
        {
            _logger.LogInformation($"Handling InstrumentPriceUpdatedEvent for instrument {@event.Symbol} with new price {@event.NewPrice}");
            // Additional handling logic can be added here
        }
    }

    public class InstrumentVolumeUpdatedEventHandler : IEventHandler<InstrumentVolumeUpdatedEvent>
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly ILogger<InstrumentVolumeUpdatedEventHandler> _logger;

        public InstrumentVolumeUpdatedEventHandler(IInstrumentRepository instrumentRepository, ILogger<InstrumentVolumeUpdatedEventHandler> logger)
        {
            _instrumentRepository = instrumentRepository;
            _logger = logger;
        }

        public async Task Handle(InstrumentVolumeUpdatedEvent @event)
        {
            _logger.LogInformation($"Handling InstrumentVolumeUpdatedEvent for instrument {@event.Symbol} with new volume {@event.NewVolume}");
            // Additional handling logic can be added here
        }
    }
} 