using Microsoft.EntityFrameworkCore;
using StockMarket.Domain.Entities;

namespace StockMarket.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<EventEntity> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Symbol).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.CurrentPrice).HasPrecision(18, 2);
                entity.Property(e => e.OpeningPrice).HasPrecision(18, 2);
                entity.Property(e => e.HighPrice).HasPrecision(18, 2);
                entity.Property(e => e.LowPrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<EventEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AggregateId).IsRequired();
                entity.Property(e => e.EventType).IsRequired();
                entity.Property(e => e.EventData).IsRequired();
                entity.Property(e => e.Version).IsRequired();
                entity.Property(e => e.OccurredOn).IsRequired();
            });
        }
    }
} 