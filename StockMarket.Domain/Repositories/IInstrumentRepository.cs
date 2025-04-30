using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMarket.Domain.Entities;

namespace StockMarket.Domain.Repositories
{
    public interface IInstrumentRepository
    {
        Task<Instrument> GetByIdAsync(Guid id);
        Task<Instrument> GetBySymbolAsync(string symbol);
        Task<IEnumerable<Instrument>> GetAllAsync();
        Task<IEnumerable<Instrument>> GetByTypeAsync(InstrumentType type);
        Task AddAsync(Instrument instrument);
        Task UpdateAsync(Instrument instrument);
    }
} 