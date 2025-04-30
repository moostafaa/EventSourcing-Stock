using Marten;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.EventStore;

namespace StockMarket.Infrastructure.Repositories
{
    public class WalletRepository : MartenRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(IDocumentSession session) : base(session)
        {
        }
    }
} 