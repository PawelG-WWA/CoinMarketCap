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
        Task<List<CryptocurrencyCard>> GetCryptocurrencyCards(int page);
    }

    public class GetCryptocurrencyCardsService : IGetCryptocurrencyCardsService
    {
        private readonly ICryptocurrencyBrowserDbContext _dbContext;
        private const int PAGE_SIZE = 9;

        public GetCryptocurrencyCardsService(ICryptocurrencyBrowserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CryptocurrencyCard>> GetCryptocurrencyCards(int page)
        {
            return await _dbContext.CryptocurrencyCards
                .OrderBy(x => x.Rank)
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .Select(x => x)
                .ToListAsync();
        }
    }
}
