using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Persistence
{
    public interface ICryptoCurrencyBrowserDbContext
    {
        DbSet<Cryptocurrency> Cryptocurrencies { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
