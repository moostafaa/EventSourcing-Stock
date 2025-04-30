using MediatR;

namespace StockMarket.Application.Commands
{
    public class CreateStockCommand : IRequest<Guid>
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal InitialPrice { get; set; }
    }
} 