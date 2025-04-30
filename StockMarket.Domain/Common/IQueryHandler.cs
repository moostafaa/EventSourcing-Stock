using System.Threading.Tasks;

namespace StockMarket.Domain.Common
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
} 