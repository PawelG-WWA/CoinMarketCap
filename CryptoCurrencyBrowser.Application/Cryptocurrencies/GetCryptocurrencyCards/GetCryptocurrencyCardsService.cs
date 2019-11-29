using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyCards
{
    public interface IGetCryptocurrencyCardsService
    {
        Task<List<CryptocurrencyCard>> GetCryptocurrencyCards();
    }

    public class GetCryptocurrencyCardsService : IGetCryptocurrencyCardsService
    {
        private readonly ICryptocurrencyBrowserDbContext _dbContext;

        public GetCryptocurrencyCardsService(ICryptocurrencyBrowserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CryptocurrencyCard>> GetCryptocurrencyCards()
        {
            return await _dbContext.CryptocurrencyCards.Select(x => x).ToListAsync();
        }
    }
}
