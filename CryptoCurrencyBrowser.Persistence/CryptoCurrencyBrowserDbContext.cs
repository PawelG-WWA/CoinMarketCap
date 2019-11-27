using Microsoft.EntityFrameworkCore;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;

namespace CryptoCurrencyBrowser.Persistence
{
    public class CryptoCurrencyBrowserDbContext : DbContext, ICryptoCurrencyBrowserDbContext
    {
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

        public CryptoCurrencyBrowserDbContext(DbContextOptions<CryptoCurrencyBrowserDbContext> options)
            : base(options)
        {
        }
    }
}
