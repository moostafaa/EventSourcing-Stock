using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Application.Commands;
using StockMarket.Domain.Repositories;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStockRepository _stockRepository;

        public StocksController(IMediator mediator, IStockRepository stockRepository)
        {
            _mediator = mediator;
            _stockRepository = stockRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockCommand command)
        {
            var stockId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetStock), new { id = stockId }, new { id = stockId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(Guid id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdateStockPrice(Guid id, [FromBody] decimal newPrice)
        {
            var command = new UpdateStockPriceCommand
            {
                StockId = id,
                NewPrice = newPrice
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
} 