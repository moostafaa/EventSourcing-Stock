using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.Common;
using StockMarket.Domain.Events.Order;
using StockMarket.Domain.Services;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Entities
{
    public class Order : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public Guid InstrumentId { get; private set; }
        public OrderType Type { get; private set; }
        public OrderSide Side { get; private set; }
        public Money Price { get; private set; }
        public Quantity TotalQuantity { get; private set; }
        public Quantity FilledQuantity { get; private set; }
        public Quantity CancelledQuantity { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string ExchangeOrderId { get; private set; }
        public string CancellationReason { get; private set; }

        private readonly List<OrderExecution> _executions = new List<OrderExecution>();
        public IReadOnlyCollection<OrderExecution> Executions => _executions.AsReadOnly();

        private readonly List<OrderState> _stateHistory = new List<OrderState>();
        public IReadOnlyCollection<OrderState> StateHistory => _stateHistory.AsReadOnly();

        private Order() { }

        public static Order Create(
            Guid accountId,
            Guid instrumentId,
            Quantity quantity,
            Money price,
            OrderType type,
            OrderSide side,
            TimeInForce timeInForce,
            OrderPriceCalculator priceCalculator)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("Account ID cannot be empty", nameof(accountId));
            if (instrumentId == Guid.Empty)
                throw new ArgumentException("Instrument ID cannot be empty", nameof(instrumentId));
            if (quantity == null)
                throw new ArgumentNullException(nameof(quantity));
            if (price == null)
                throw new ArgumentNullException(nameof(price));
            if (priceCalculator == null)
                throw new ArgumentNullException(nameof(priceCalculator));

            var order = new Order
            {
                AccountId = accountId,
                InstrumentId = instrumentId,
                Type = type,
                Side = side,
                Price = price,
                TotalQuantity = quantity,
                FilledQuantity = new Quantity(0),
                CancelledQuantity = new Quantity(0),
                Status = OrderStatus.New,
                CreatedAt = DateTime.UtcNow
            };

            var priceCalculation = priceCalculator.Calculate(order);
            order.AddDomainEvent(new OrderCreatedEvent(
                order.Id,
                accountId,
                instrumentId,
                type,
                side,
                price,
                quantity,
                order.CreatedAt,
                priceCalculation
            ));

            order._stateHistory.Add(new OrderState(order));

            return order;
        }

        public void MarkAsSentToExchange(string exchangeOrderId)
        {
            if (string.IsNullOrWhiteSpace(exchangeOrderId))
                throw new ArgumentException("Exchange order ID cannot be empty", nameof(exchangeOrderId));
            if (Status != OrderStatus.New)
                throw new InvalidOperationException("Order must be in New status to be sent to exchange");

            Status = OrderStatus.SentToExchange;
            ExchangeOrderId = exchangeOrderId;

            _stateHistory.Add(new OrderState(this));
            AddDomainEvent(new OrderSentToExchangeEvent(Id, exchangeOrderId));
        }

        public void UpdateFilledQuantity(Quantity quantity, Money executionPrice)
        {
            if (Status != OrderStatus.SentToExchange && Status != OrderStatus.PartiallyFilled)
                throw new InvalidOperationException("Order must be in SentToExchange or PartiallyFilled status");

            var remainingQuantity = TotalQuantity - FilledQuantity - CancelledQuantity;
            if (quantity > remainingQuantity)
                throw new ArgumentException("Filled quantity cannot exceed remaining quantity");

            var execution = new OrderExecution(quantity, executionPrice, DateTime.UtcNow);
            _executions.Add(execution);
            FilledQuantity += quantity;

            if (FilledQuantity + CancelledQuantity == TotalQuantity)
            {
                Status = OrderStatus.Done;
                AddDomainEvent(new OrderCompletedEvent(Id, FilledQuantity, CancelledQuantity));
            }
            else
            {
                Status = OrderStatus.PartiallyFilled;
                AddDomainEvent(new OrderPartiallyFilledEvent(Id, quantity, executionPrice));
            }

            _stateHistory.Add(new OrderState(this));
        }

        public void Cancel(Quantity quantity, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Cancellation reason cannot be empty", nameof(reason));
            if (Status == OrderStatus.Done || Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot cancel completed or cancelled order");

            var remainingQuantity = TotalQuantity - FilledQuantity - CancelledQuantity;
            if (quantity > remainingQuantity)
                throw new ArgumentException("Cancellation quantity cannot exceed remaining quantity");

            CancelledQuantity += quantity;
            CancellationReason = reason;

            if (FilledQuantity + CancelledQuantity == TotalQuantity)
            {
                Status = OrderStatus.Done;
                AddDomainEvent(new OrderCompletedEvent(Id, FilledQuantity, CancelledQuantity));
            }
            else
            {
                Status = OrderStatus.PartiallyCancelled;
                AddDomainEvent(new OrderPartiallyCancelledEvent(Id, quantity, reason));
            }

            _stateHistory.Add(new OrderState(this));
        }

        public void Edit(Money newPrice, Quantity newQuantity, OrderPriceCalculator priceCalculator)
        {
            if (newPrice == null)
                throw new ArgumentNullException(nameof(newPrice));
            if (newQuantity == null)
                throw new ArgumentNullException(nameof(newQuantity));
            if (priceCalculator == null)
                throw new ArgumentNullException(nameof(priceCalculator));
            if (Status != OrderStatus.New && Status != OrderStatus.SentToExchange)
                throw new InvalidOperationException("Can only edit new or sent orders");

            var minimumQuantity = FilledQuantity + CancelledQuantity;
            if (newQuantity < minimumQuantity)
                throw new ArgumentException($"New quantity cannot be less than {minimumQuantity}");

            var oldPrice = Price;
            var oldQuantity = TotalQuantity;

            Price = newPrice;
            TotalQuantity = newQuantity;

            var priceCalculation = priceCalculator.Calculate(this);
            _stateHistory.Add(new OrderState(this));
            AddDomainEvent(new OrderEditedEvent(
                Id,
                oldPrice,
                oldQuantity,
                newPrice,
                newQuantity,
                priceCalculation
            ));
        }

        public Money CalculateRequiredBlockedAmount(OrderPriceCalculator priceCalculator)
        {
            if (priceCalculator == null)
                throw new ArgumentNullException(nameof(priceCalculator));
            if (Side == OrderSide.Sell)
                return new Money(0, Price.Currency);

            var priceCalculation = priceCalculator.Calculate(this);
            return priceCalculation.TotalAmount;
        }

        public Money CalculateAverageExecutionPrice()
        {
            if (!_executions.Any())
                return null;

            var totalValue = _executions.Sum(e => e.TotalValue.Amount);
            var totalQuantity = _executions.Sum(e => e.Quantity.Value);
            return new Money(totalValue / totalQuantity, Price.Currency);
        }

        // Derived properties from event history
        public DateTime? SentToExchangeAt => GetFirstEventTimestamp<OrderSentToExchangeEvent>();
        public DateTime? FirstFillAt => GetFirstEventTimestamp<OrderPartiallyFilledEvent>();
        public DateTime? LastFillAt => GetLastEventTimestamp<OrderPartiallyFilledEvent>();
        public DateTime? CancelledAt => GetFirstEventTimestamp<OrderPartiallyCancelledEvent>();

        private DateTime? GetFirstEventTimestamp<T>() where T : DomainEvent
        {
            return DomainEvents
                .OfType<T>()
                .OrderBy(e => e.OccurredOn)
                .FirstOrDefault()?.OccurredOn;
        }

        private DateTime? GetLastEventTimestamp<T>() where T : DomainEvent
        {
            return DomainEvents
                .OfType<T>()
                .OrderByDescending(e => e.OccurredOn)
                .FirstOrDefault()?.OccurredOn;
        }
    }

    public class OrderState
    {
        public DateTime Timestamp { get; }
        public Guid AccountId { get; }
        public Guid InstrumentId { get; }
        public OrderType Type { get; }
        public OrderSide Side { get; }
        public Money Price { get; }
        public Quantity TotalQuantity { get; }
        public Quantity FilledQuantity { get; }
        public Quantity CancelledQuantity { get; }
        public OrderStatus Status { get; }
        public string ExchangeOrderId { get; }
        public string CancellationReason { get; }
        public IReadOnlyCollection<OrderExecution> Executions { get; }

        public OrderState(Order order)
        {
            Timestamp = DateTime.UtcNow;
            AccountId = order.AccountId;
            InstrumentId = order.InstrumentId;
            Type = order.Type;
            Side = order.Side;
            Price = order.Price;
            TotalQuantity = order.TotalQuantity;
            FilledQuantity = order.FilledQuantity;
            CancelledQuantity = order.CancelledQuantity;
            Status = order.Status;
            ExchangeOrderId = order.ExchangeOrderId;
            CancellationReason = order.CancellationReason;
            Executions = order.Executions;
        }
    }

    public enum OrderType
    {
        Market,
        Limit,
        StopLoss,
        StopLimit
    }

    public enum OrderSide
    {
        Buy,
        Sell
    }

    public enum OrderStatus
    {
        New,
        SentToExchange,
        PartiallyFilled,
        PartiallyCancelled,
        Done,
        Cancelled
    }
} 