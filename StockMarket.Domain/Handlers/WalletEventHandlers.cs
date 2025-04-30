using System;
using System.Threading.Tasks;
using StockMarket.Domain.Events.Wallet;
using StockMarket.Domain.ReadModels;

namespace StockMarket.Domain.Handlers
{
    public class WalletEventHandlers
    {
        private readonly IReadModelRepository<WalletReadModel> _walletReadModelRepository;

        public WalletEventHandlers(IReadModelRepository<WalletReadModel> walletReadModelRepository)
        {
            _walletReadModelRepository = walletReadModelRepository ?? throw new ArgumentNullException(nameof(walletReadModelRepository));
        }

        public async Task Handle(WalletCreatedEvent @event)
        {
            var readModel = new WalletReadModel(@event.AccountId);
            readModel.ApplyWalletCreated(@event.InitialBalance);
            await _walletReadModelRepository.Save(@event.AccountId, readModel);
        }

        public async Task Handle(AmountBlockedEvent @event)
        {
            var readModel = await _walletReadModelRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.ApplyAmountBlocked(@event.Amount);
                await _walletReadModelRepository.Save(@event.AggregateId, readModel);
            }
        }

        public async Task Handle(AmountUnblockedEvent @event)
        {
            var readModel = await _walletReadModelRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.ApplyAmountUnblocked(@event.Amount);
                await _walletReadModelRepository.Save(@event.AggregateId, readModel);
            }
        }

        public async Task Handle(TransactionAddedEvent @event)
        {
            var readModel = await _walletReadModelRepository.Get(@event.AggregateId);
            if (readModel != null)
            {
                readModel.ApplyTransactionAdded(@event.Transaction);
                await _walletReadModelRepository.Save(@event.AggregateId, readModel);
            }
        }
    }
} 