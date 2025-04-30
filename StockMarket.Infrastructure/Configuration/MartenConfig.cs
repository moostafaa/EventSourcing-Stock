using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.Entities;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Infrastructure.Configuration
{
    public static class MartenConfig
    {
        public static void AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            
            services.AddMarten(options =>
            {
                options.Connection(connectionString);
                
                // Enable snapshots
                options.Events.EnableProjections();
                options.Events.EnableSnapshots();
                
                // Register document types
                options.Schema.For<OrderBookProjection>();
                options.Schema.For<PortfolioReadModel>();
                options.Schema.For<WalletReadModel>();
                
                // Register event types
                options.Events.AddEventType(typeof(OrderCreatedEvent));
                options.Events.AddEventType(typeof(OrderEditedEvent));
                options.Events.AddEventType(typeof(OrderCancelledEvent));
                options.Events.AddEventType(typeof(OrderFilledEvent));
                options.Events.AddEventType(typeof(OrderPartiallyFilledEvent));
                
                options.Events.AddEventType(typeof(InstrumentCreatedEvent));
                options.Events.AddEventType(typeof(InstrumentInfoUpdatedEvent));
                options.Events.AddEventType(typeof(InstrumentPriceUpdatedEvent));
                options.Events.AddEventType(typeof(InstrumentVolumeUpdatedEvent));
                
                options.Events.AddEventType(typeof(PortfolioCreatedEvent));
                options.Events.AddEventType(typeof(InstrumentAddedToPortfolioEvent));
                options.Events.AddEventType(typeof(InstrumentRemovedFromPortfolioEvent));
                
                options.Events.AddEventType(typeof(WalletCreatedEvent));
                options.Events.AddEventType(typeof(AmountBlockedEvent));
                options.Events.AddEventType(typeof(AmountUnblockedEvent));
                options.Events.AddEventType(typeof(TransactionAddedEvent));
                
                options.Events.AddEventType(typeof(TradeCreatedEvent));
                options.Events.AddEventType(typeof(TradeSettledEvent));
                
                // Configure snapshot strategy
                options.Events.SnapshotStrategy = new SnapshotStrategy
                {
                    ShouldCreateSnapshot = (aggregate, events) => events.Count >= 50
                };
            });
        }
    }
} 