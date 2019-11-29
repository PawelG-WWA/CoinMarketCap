using Microsoft.EntityFrameworkCore;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;
using CryptoCurrencyBrowser.Persistence.Configurations;

namespace CryptoCurrencyBrowser.Persistence
{
    public class CryptocurrencyBrowserDbContext : DbContext, ICryptocurrencyBrowserDbContext
    {
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<CryptocurrencyCard> CryptocurrencyCards { get; set; }

        public CryptocurrencyBrowserDbContext(DbContextOptions<CryptocurrencyBrowserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CryptocurrencyCardsConfiguration).Assembly);
        }
    }
}
