using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Trade;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.EventHandlers
{
    public class TradeEventHandlers
    {
        private readonly TradeReadModel _tradeReadModel;

        public TradeEventHandlers(TradeReadModel tradeReadModel)
        {
            _tradeReadModel = tradeReadModel;
        }

        public Task Handle(TradeCreatedEvent @event)
        {
            _tradeReadModel.AddTrade(
                @event.TradeId,
                @event.BuyOrderId,
                @event.SellOrderId,
                @event.InstrumentId,
                @event.Price,
                @event.Quantity,
                @event.Timestamp
            );
            return Task.CompletedTask;
        }
    }
} 