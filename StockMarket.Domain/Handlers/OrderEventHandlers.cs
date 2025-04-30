using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Order;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.Handlers
{
    public class OrderEventHandlers
    {
        private readonly IReadModelRepository<OrderBookProjection> _orderBookRepository;
        private readonly IReadModelRepository<WalletReadModel> _walletRepository;

        public OrderEventHandlers(
            IReadModelRepository<OrderBookProjection> orderBookRepository,
            IReadModelRepository<WalletReadModel> walletRepository)
        {
            _orderBookRepository = orderBookRepository ?? throw new ArgumentNullException(nameof(orderBookRepository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task Handle(OrderCreatedEvent @event)
        {
            // Update order book
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook == null)
            {
                orderBook = new OrderBookProjection(@event.InstrumentId);
            }

            if (@event.Side == OrderSide.Buy)
            {
                orderBook.AddBuyOrder(@event.AggregateId, @event.Price, @event.Quantity);
            }
            else
            {
                orderBook.AddSellOrder(@event.AggregateId, @event.Price, @event.Quantity);
            }

            await _orderBookRepository.Save(@event.InstrumentId, orderBook);

            // Block amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    wallet.ApplyAmountBlocked(@event.PriceCalculation.TotalAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }

        public async Task Handle(OrderEditedEvent @event)
        {
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook != null)
            {
                orderBook.UpdateOrder(@event.AggregateId, @event.NewPrice, @event.NewQuantity);
                await _orderBookRepository.Save(@event.InstrumentId, orderBook);
            }

            // Update blocked amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    // Unblock old amount
                    wallet.ApplyAmountUnblocked(@event.OldPriceCalculation.TotalAmount);
                    // Block new amount
                    wallet.ApplyAmountBlocked(@event.NewPriceCalculation.TotalAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }

        public async Task Handle(OrderCancelledEvent @event)
        {
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook != null)
            {
                orderBook.RemoveOrder(@event.AggregateId);
                await _orderBookRepository.Save(@event.InstrumentId, orderBook);
            }

            // Unblock amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    wallet.ApplyAmountUnblocked(@event.BlockedAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }

        public async Task Handle(OrderPartiallyFilledEvent @event)
        {
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook != null)
            {
                orderBook.UpdateOrder(@event.AggregateId, @event.Price, @event.RemainingQuantity);
                await _orderBookRepository.Save(@event.InstrumentId, orderBook);
            }

            // Update blocked amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    // Calculate the amount to unblock (filled quantity * price)
                    var unblockAmount = @event.Price * @event.FilledQuantity.Value;
                    wallet.ApplyAmountUnblocked(unblockAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }

        public async Task Handle(OrderCompletedEvent @event)
        {
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook != null)
            {
                orderBook.RemoveOrder(@event.AggregateId);
                await _orderBookRepository.Save(@event.InstrumentId, orderBook);
            }

            // Unblock any remaining amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    // Calculate the remaining blocked amount
                    var remainingBlockedAmount = @event.Price * @event.RemainingQuantity.Value;
                    wallet.ApplyAmountUnblocked(remainingBlockedAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }

        public async Task Handle(OrderSentToExchangeEvent @event)
        {
            // This event is mainly for tracking and doesn't require immediate read model updates
            // Could be used for analytics or reporting purposes
        }

        public async Task Handle(OrderPartiallyCancelledEvent @event)
        {
            var orderBook = await _orderBookRepository.Get(@event.InstrumentId);
            if (orderBook != null)
            {
                orderBook.UpdateOrder(@event.AggregateId, @event.Price, @event.RemainingQuantity);
                await _orderBookRepository.Save(@event.InstrumentId, orderBook);
            }

            // Unblock cancelled amount in wallet for buy orders
            if (@event.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(@event.AccountId);
                if (wallet != null)
                {
                    // Calculate the amount to unblock (cancelled quantity * price)
                    var unblockAmount = @event.Price * @event.CancelledQuantity.Value;
                    wallet.ApplyAmountUnblocked(unblockAmount);
                    await _walletRepository.Save(@event.AccountId, wallet);
                }
            }
        }
    }
} 