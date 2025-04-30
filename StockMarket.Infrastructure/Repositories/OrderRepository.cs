using Marten;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.EventStore;

namespace StockMarket.Infrastructure.Repositories
{
    public class OrderRepository : MartenRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDocumentSession session) : base(session)
        {
        }
    }
} 