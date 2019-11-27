using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoCurrencyBrowser.Persistence
{
    public class CryptoCurrencyBrowserDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CryptoCurrencyBrowserDbContext>
    {
        public CryptoCurrencyBrowserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptoCurrencyBrowserDbContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = CryptoCurrencyBrowser; Trusted_Connection = True;");

            return new CryptoCurrencyBrowserDbContext(optionsBuilder.Options);
        }
    }
}
