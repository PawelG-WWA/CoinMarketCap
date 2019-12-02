using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Persistence
{
    public interface ICryptocurrencyBrowserDbContext
    {
        DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        DbQuery<CryptocurrencyCard> CryptocurrencyCards { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void UpdateRange(params object[] entities);
    }
}
