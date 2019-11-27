using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoCurrencyBrowser.Application.Persistence
{
    public interface ICryptoCurrencyBrowserDbContext
    {
        DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    }
}
