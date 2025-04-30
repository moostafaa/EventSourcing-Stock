using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Trade;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.Handlers
{
    public class TradeEventHandlers
    {
        private readonly IReadModelRepository<PortfolioReadModel> _portfolioRepository;
        private readonly IReadModelRepository<WalletReadModel> _walletRepository;

        public TradeEventHandlers(
            IReadModelRepository<PortfolioReadModel> portfolioRepository,
            IReadModelRepository<WalletReadModel> walletRepository)
        {
            _portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task Handle(TradeExecutedEvent @event)
        {
            // Update buyer's portfolio
            var buyerPortfolio = await _portfolioRepository.Get(@event.BuyerAccountId);
            if (buyerPortfolio != null)
            {
                buyerPortfolio.AddInstrument(@event.InstrumentId, @event.Quantity, @event.Price);
                await _portfolioRepository.Save(@event.BuyerAccountId, buyerPortfolio);
            }

            // Update seller's portfolio
            var sellerPortfolio = await _portfolioRepository.Get(@event.SellerAccountId);
            if (sellerPortfolio != null)
            {
                sellerPortfolio.RemoveInstrument(@event.InstrumentId, @event.Quantity);
                await _portfolioRepository.Save(@event.SellerAccountId, sellerPortfolio);
            }

            // Update buyer's wallet
            var buyerWallet = await _walletRepository.Get(@event.BuyerAccountId);
            if (buyerWallet != null)
            {
                // Unblock the amount that was blocked for the order
                buyerWallet.ApplyAmountUnblocked(@event.TotalAmount);
                // Add commission transaction
                buyerWallet.ApplyTransactionAdded(new WalletTransaction(
                    WalletTransactionType.Commission,
                    @event.BuyerCommission,
                    $"Commission for trade {@event.AggregateId}",
                    @event.BuyOrderId
                ));
                await _walletRepository.Save(@event.BuyerAccountId, buyerWallet);
            }

            // Update seller's wallet
            var sellerWallet = await _walletRepository.Get(@event.SellerAccountId);
            if (sellerWallet != null)
            {
                // Add the sale amount
                sellerWallet.ApplyTransactionAdded(new WalletTransaction(
                    WalletTransactionType.Deposit,
                    @event.TotalAmount - @event.SellerCommission,
                    $"Sale of {@event.Quantity.Value} shares at {@event.Price.Amount}",
                    @event.SellOrderId
                ));
                // Add commission transaction
                sellerWallet.ApplyTransactionAdded(new WalletTransaction(
                    WalletTransactionType.Commission,
                    @event.SellerCommission,
                    $"Commission for trade {@event.AggregateId}",
                    @event.SellOrderId
                ));
                await _walletRepository.Save(@event.SellerAccountId, sellerWallet);
            }
        }
    }
} 