using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate
{
    public interface IAddOrUpdateService
    {
        Task AddOrUpdateCryptocurrencies(IList<AddOrUpdateModel> createModel);
    }

    public class AddOrUpdateService : IAddOrUpdateService
    {
        private readonly ICryptoCurrencyBrowserDbContext _dbContext;
        private readonly ILogger<AddOrUpdateService> _logger;

        public AddOrUpdateService(ICryptoCurrencyBrowserDbContext dbContext,
            ILogger<AddOrUpdateService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddOrUpdateCryptocurrencies(IList<AddOrUpdateModel> createModel)
        {
            var entities = new List<Cryptocurrency>();

            foreach(var model in createModel)
            {
                entities.Add(new Cryptocurrency
                {
                    CirculatingSupply = model.CirculatingSupply,
                    CurrentPrice = model.Price,
                    ExternalId = model.Id,
                    LastUpdated = model.LastUpdated,
                    MarketCap = model.MarketCap,
                    MaxSupply = model.MaxSupply,
                    Name = model.Name,
                    PercentChange1h = model.PercentChange1h,
                    PercentChange24h = model.PercentChange24h,
                    PercentChange7d = model.PercentChange7d,
                    Rank = model.CMCRank,
                    Symbol = model.Symbol,
                    TotalSupply = model.TotalSupply,
                    Volume24h = model.Volume24h
                });
            }

            _logger.LogInformation("Saving cryptocurrencies to the database");

            await _dbContext.Cryptocurrencies.AddRangeAsync(entities).ConfigureAwait(false);

            await _dbContext.SaveChangesAsync();
        }
    }
}
