using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StockMarket.Application.Commands;
using StockMarket.Domain.Entities;
using StockMarket.Domain.Repositories;

namespace StockMarket.Application.CommandHandlers
{
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, Guid>
    {
        private readonly IStockRepository _stockRepository;

        public CreateStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Guid> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new Stock(request.Symbol, request.Name, request.InitialPrice);
            await _stockRepository.AddAsync(stock);
            return stock.Id;
        }
    }
} 