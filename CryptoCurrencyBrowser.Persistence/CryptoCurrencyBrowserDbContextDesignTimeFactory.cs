using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoCurrencyBrowser.Persistence
{
    public class CryptocurrencyBrowserDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CryptocurrencyBrowserDbContext>
    {
        public CryptocurrencyBrowserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptocurrencyBrowserDbContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = CryptoCurrencyBrowser; Trusted_Connection = True;");

            return new CryptocurrencyBrowserDbContext(optionsBuilder.Options);
        }
    }
}
