using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyDetails
{
    public interface IGetCryptocurrencyDetailsService
    {
        // I know I return entity directly which is not a perfect
        // practice, but the project is simple, so to keep simplicity
        // I decided to send the entity as a model to the user
        Task<Cryptocurrency> GetDetailsById(int cryptocurrencyId);
    }

    public class GetCryptocurrencyDetailsService : IGetCryptocurrencyDetailsService
    {
        private readonly ICryptocurrencyBrowserDbContext _dbContext;

        public GetCryptocurrencyDetailsService(ICryptocurrencyBrowserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cryptocurrency> GetDetailsById(int cryptocurrencyId)
        {
            return await _dbContext.Cryptocurrencies.FirstOrDefaultAsync(x => x.Id == cryptocurrencyId);
        }
    }
}
