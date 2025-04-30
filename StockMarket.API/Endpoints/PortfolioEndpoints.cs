using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StockMarket.Domain.Aggregates;
using StockMarket.Domain.ValueObjects;
using StockMarket.Domain.Services;
using StockMarket.Domain.EventStore;

namespace StockMarket.API.Endpoints
{
    public static class PortfolioEndpoints
    {
        public static void MapPortfolioEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/portfolios")
                .WithTags("Portfolios")
                .WithOpenApi();

            group.MapPost("/{id}/instruments", async (
                Guid id,
                AddInstrumentRequest request,
                IPortfolioRepository portfolioRepository) =>
            {
                var portfolio = await portfolioRepository.GetByIdAsync(id);
                portfolio.AddInstrument(
                    request.InstrumentId,
                    new Quantity(request.Quantity));

                await portfolioRepository.SaveAsync(portfolio);

                return Results.Ok(new PortfolioResponse(portfolio));
            })
            .WithName("AddInstrument")
            .WithDescription("Adds an instrument to a portfolio")
            .Produces<PortfolioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id}/instruments/{instrumentId}", async (
                Guid id,
                Guid instrumentId,
                IPortfolioRepository portfolioRepository) =>
            {
                var portfolio = await portfolioRepository.GetByIdAsync(id);
                portfolio.RemoveInstrument(instrumentId);

                await portfolioRepository.SaveAsync(portfolio);

                return Results.Ok(new PortfolioResponse(portfolio));
            })
            .WithName("RemoveInstrument")
            .WithDescription("Removes an instrument from a portfolio")
            .Produces<PortfolioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{id}", async (
                Guid id,
                IPortfolioRepository portfolioRepository) =>
            {
                var portfolio = await portfolioRepository.GetByIdAsync(id);
                return Results.Ok(new PortfolioResponse(portfolio));
            })
            .WithName("GetPortfolio")
            .WithDescription("Gets a portfolio by ID")
            .Produces<PortfolioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }

    public record AddInstrumentRequest(
        Guid InstrumentId,
        decimal Quantity);

    public record PortfolioResponse(
        Guid Id,
        Guid AccountId,
        string Name,
        IReadOnlyDictionary<Guid, decimal> Instruments)
    {
        public PortfolioResponse(Portfolio portfolio) : this(
            portfolio.Id,
            portfolio.AccountId,
            portfolio.Name,
            portfolio.Instruments.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Value))
        {
        }
    }
} 