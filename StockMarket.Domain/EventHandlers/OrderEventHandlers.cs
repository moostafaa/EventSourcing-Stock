using System;
using System.Threading.Tasks;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.Events.Order;
using StockMarket.Domain.ReadModels;
using StockMarket.Domain.Services;

namespace StockMarket.Domain.EventHandlers
{
    public class OrderEventHandlers
    {
        private readonly OrderBookProjection _orderBookProjection;
        private readonly IWalletService _walletService;

        public OrderEventHandlers(OrderBookProjection orderBookProjection, IWalletService walletService)
        {
            _orderBookProjection = orderBookProjection;
            _walletService = walletService;
        }

        public async Task Handle(OrderCreatedEvent @event)
        {
            // Update order book
            _orderBookProjection.AddOrder(@event.OrderId, @event.InstrumentId, @event.Side, @event.Price, @event.Quantity);

            // Block funds for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                await _walletService.BlockAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
            }
        }

        public async Task Handle(OrderEditedEvent @event)
        {
            // Update order book
            _orderBookProjection.UpdateOrder(@event.OrderId, @event.Price, @event.Quantity);

            // Update blocked funds for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                await _walletService.UpdateBlockedAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
            }
        }

        public async Task Handle(OrderCancelledEvent @event)
        {
            // Remove from order book
            _orderBookProjection.RemoveOrder(@event.OrderId);

            // Release blocked funds for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                await _walletService.ReleaseBlockedAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
            }
        }

        public async Task Handle(OrderFilledEvent @event)
        {
            // Remove from order book
            _orderBookProjection.RemoveOrder(@event.OrderId);

            // Process funds
            if (@event.Side == OrderSide.Buy)
            {
                // Release blocked funds and deduct actual cost
                await _walletService.ReleaseBlockedAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
                await _walletService.DeductAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
            }
            else
            {
                // Add funds from sale
                await _walletService.AddAmountAsync(@event.AccountId, @event.Price * @event.Quantity);
            }
        }
    }
} 