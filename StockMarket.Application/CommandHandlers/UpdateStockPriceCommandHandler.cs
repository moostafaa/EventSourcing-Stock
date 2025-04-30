using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockMarket.Application.Commands;
using StockMarket.Domain.Repositories;

namespace StockMarket.Application.CommandHandlers
{
    public class UpdateStockPriceCommandHandler : IRequestHandler<UpdateStockPriceCommand>
    {
        private readonly IStockRepository _stockRepository;

        public UpdateStockPriceCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Unit> Handle(UpdateStockPriceCommand request, CancellationToken cancellationToken)
        {
            var stock = await _stockRepository.GetByIdAsync(request.StockId);
            if (stock == null)
                throw new ArgumentException($"Stock with ID {request.StockId} not found");

            stock.UpdatePrice(request.NewPrice);
            await _stockRepository.UpdateAsync(stock);
            return Unit.Value;
        }
    }
} 