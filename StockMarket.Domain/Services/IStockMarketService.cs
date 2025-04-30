using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Services
{
    public interface IStockMarketService
    {
        // Order operations
        Task<Order> CreateOrder(CreateOrderCommand command);
        Task EditOrder(EditOrderCommand command);
        Task CancelOrder(CancelOrderCommand command);

        // Portfolio operations
        Task<Portfolio> CreatePortfolio(CreatePortfolioCommand command);
        Task AddInstrumentToPortfolio(AddInstrumentToPortfolioCommand command);
        Task RemoveInstrumentFromPortfolio(RemoveInstrumentFromPortfolioCommand command);

        // Wallet operations
        Task<Wallet> CreateWallet(CreateWalletCommand command);
        Task DepositToWallet(DepositToWalletCommand command);
        Task WithdrawFromWallet(WithdrawFromWalletCommand command);

        // Queries
        Task<OrderBookProjection> GetOrderBook(Guid instrumentId);
        Task<PortfolioReadModel> GetPortfolio(Guid accountId);
        Task<WalletReadModel> GetWallet(Guid accountId);
        Task<List<Order>> GetAccountOrders(Guid accountId);
        Task<List<Trade>> GetAccountTrades(Guid accountId);
        Task<Money> GetAvailableBalance(Guid accountId);
    }
} 