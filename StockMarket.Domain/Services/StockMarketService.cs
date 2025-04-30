using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.Common;
using StockMarket.Domain.ReadModels;
using StockMarket.Domain.Repositories;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Services
{
    public class StockMarketService : IStockMarketService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IReadModelRepository<OrderBookProjection> _orderBookRepository;
        private readonly IReadModelRepository<PortfolioReadModel> _portfolioReadModelRepository;
        private readonly IReadModelRepository<WalletReadModel> _walletReadModelRepository;
        private readonly OrderPriceCalculator _priceCalculator;

        public StockMarketService(
            IOrderRepository orderRepository,
            IPortfolioRepository portfolioRepository,
            IWalletRepository walletRepository,
            IReadModelRepository<OrderBookProjection> orderBookRepository,
            IReadModelRepository<PortfolioReadModel> portfolioReadModelRepository,
            IReadModelRepository<WalletReadModel> walletReadModelRepository,
            OrderPriceCalculator priceCalculator)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _orderBookRepository = orderBookRepository ?? throw new ArgumentNullException(nameof(orderBookRepository));
            _portfolioReadModelRepository = portfolioReadModelRepository ?? throw new ArgumentNullException(nameof(portfolioReadModelRepository));
            _walletReadModelRepository = walletReadModelRepository ?? throw new ArgumentNullException(nameof(walletReadModelRepository));
            _priceCalculator = priceCalculator ?? throw new ArgumentNullException(nameof(priceCalculator));
        }

        public async Task<Order> CreateOrder(CreateOrderCommand command)
        {
            // Validate wallet balance for buy orders
            if (command.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(command.AccountId);
                if (wallet == null)
                    throw new InvalidOperationException("Wallet not found");

                var requiredAmount = _priceCalculator.Calculate(new Order(
                    command.AccountId,
                    command.InstrumentId,
                    command.Type,
                    command.Side,
                    command.Price,
                    command.Quantity,
                    _priceCalculator
                )).TotalAmount;

                if (wallet.GetAvailableBalance() < requiredAmount)
                    throw new InvalidOperationException("Insufficient balance");
            }

            var order = new Order(
                command.AccountId,
                command.InstrumentId,
                command.Type,
                command.Side,
                command.Price,
                command.Quantity,
                _priceCalculator
            );

            await _orderRepository.Save(order);
            return order;
        }

        public async Task EditOrder(EditOrderCommand command)
        {
            var order = await _orderRepository.Get(command.OrderId);
            if (order == null)
                throw new InvalidOperationException("Order not found");

            // Validate wallet balance for buy orders
            if (order.Side == OrderSide.Buy)
            {
                var wallet = await _walletRepository.Get(order.AccountId);
                if (wallet == null)
                    throw new InvalidOperationException("Wallet not found");

                var requiredAmount = _priceCalculator.Calculate(new Order(
                    order.AccountId,
                    order.InstrumentId,
                    order.Type,
                    order.Side,
                    command.NewPrice,
                    command.NewQuantity,
                    _priceCalculator
                )).TotalAmount;

                if (wallet.GetAvailableBalance() < requiredAmount)
                    throw new InvalidOperationException("Insufficient balance");
            }

            order.Edit(command.NewPrice, command.NewQuantity, _priceCalculator);
            await _orderRepository.Save(order);
        }

        public async Task CancelOrder(CancelOrderCommand command)
        {
            var order = await _orderRepository.Get(command.OrderId);
            if (order == null)
                throw new InvalidOperationException("Order not found");

            order.Cancel(command.Reason);
            await _orderRepository.Save(order);
        }

        public async Task<Portfolio> CreatePortfolio(CreatePortfolioCommand command)
        {
            var portfolio = new Portfolio(command.AccountId);
            await _portfolioRepository.Save(portfolio);
            return portfolio;
        }

        public async Task AddInstrumentToPortfolio(AddInstrumentToPortfolioCommand command)
        {
            var portfolio = await _portfolioRepository.Get(command.PortfolioId);
            if (portfolio == null)
                throw new InvalidOperationException("Portfolio not found");

            portfolio.AddInstrument(command.InstrumentId, command.Quantity, command.Price);
            await _portfolioRepository.Save(portfolio);
        }

        public async Task RemoveInstrumentFromPortfolio(RemoveInstrumentFromPortfolioCommand command)
        {
            var portfolio = await _portfolioRepository.Get(command.PortfolioId);
            if (portfolio == null)
                throw new InvalidOperationException("Portfolio not found");

            portfolio.RemoveInstrument(command.InstrumentId, command.Quantity);
            await _portfolioRepository.Save(portfolio);
        }

        public async Task<Wallet> CreateWallet(CreateWalletCommand command)
        {
            var wallet = new Wallet(command.AccountId, command.InitialBalance);
            await _walletRepository.Save(wallet);
            return wallet;
        }

        public async Task DepositToWallet(DepositToWalletCommand command)
        {
            var wallet = await _walletRepository.Get(command.AccountId);
            if (wallet == null)
                throw new InvalidOperationException("Wallet not found");

            wallet.AddTransaction(new WalletTransaction(
                WalletTransactionType.Deposit,
                command.Amount,
                command.Description
            ));

            await _walletRepository.Save(wallet);
        }

        public async Task WithdrawFromWallet(WithdrawFromWalletCommand command)
        {
            var wallet = await _walletRepository.Get(command.AccountId);
            if (wallet == null)
                throw new InvalidOperationException("Wallet not found");

            wallet.AddTransaction(new WalletTransaction(
                WalletTransactionType.Withdrawal,
                command.Amount,
                command.Description
            ));

            await _walletRepository.Save(wallet);
        }

        public async Task<OrderBookProjection> GetOrderBook(Guid instrumentId)
        {
            return await _orderBookRepository.Get(instrumentId);
        }

        public async Task<PortfolioReadModel> GetPortfolio(Guid accountId)
        {
            return await _portfolioReadModelRepository.Get(accountId);
        }

        public async Task<WalletReadModel> GetWallet(Guid accountId)
        {
            return await _walletReadModelRepository.Get(accountId);
        }

        public async Task<List<Order>> GetAccountOrders(Guid accountId)
        {
            return await _orderRepository.GetByAccountId(accountId);
        }

        public async Task<List<Trade>> GetAccountTrades(Guid accountId)
        {
            return await _orderRepository.GetTradesByAccountId(accountId);
        }

        public async Task<Money> GetAvailableBalance(Guid accountId)
        {
            var wallet = await _walletReadModelRepository.Get(accountId);
            if (wallet == null)
                throw new InvalidOperationException("Wallet not found");

            return wallet.AvailableBalance;
        }
    }
} 