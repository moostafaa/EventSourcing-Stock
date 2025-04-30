using MediatR;

namespace StockMarket.Application.Commands
{
    public class UpdateStockPriceCommand : IRequest
    {
        public Guid StockId { get; set; }
        public decimal NewPrice { get; set; }
    }
} 