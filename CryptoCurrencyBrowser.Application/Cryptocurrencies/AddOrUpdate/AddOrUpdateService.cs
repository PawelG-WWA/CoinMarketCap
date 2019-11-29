using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate
{
    public interface IAddOrUpdateService
    {
        Task AddOrUpdateCryptocurrencies(IList<AddOrUpdateModel> createModel);
    }

    public class AddOrUpdateService : IAddOrUpdateService
    {
        private readonly ICryptocurrencyBrowserDbContext _dbContext;
        private readonly ILogger<AddOrUpdateService> _logger;

        public AddOrUpdateService(ICryptocurrencyBrowserDbContext dbContext,
            ILogger<AddOrUpdateService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddOrUpdateCryptocurrencies(IList<AddOrUpdateModel> addOrUpdateModel)
        {
            var ids = addOrUpdateModel.Select(x => x.Id).ToList();

            var entitiesToUpdate = _dbContext.Cryptocurrencies.Where(x => ids.Contains(x.ExternalId)).ToList();

            var existingExternalIds = entitiesToUpdate.Select(x => x.ExternalId).ToList();

            var updateModels = addOrUpdateModel.Where(x => existingExternalIds.Contains(x.Id)).ToList();

            var nonExistingExternalIds = ids.Except(existingExternalIds);

            var nonExistingModels = addOrUpdateModel.Where(x => nonExistingExternalIds.Contains(x.Id)).ToList();

            UpdateEntities(updateModels, entitiesToUpdate);

            await CreateEntities(nonExistingModels);

            _logger.LogInformation("Saving cryptocurrencies to the database");

            await _dbContext.SaveChangesAsync();
        }

        private void UpdateEntities(IList<AddOrUpdateModel> updateModels, IList<Cryptocurrency> entitiesToUpdate)
        {
            foreach (var entityToUpdate in entitiesToUpdate)
            {
                var updateModel = updateModels.First(x => x.Id == entityToUpdate.ExternalId);
                entityToUpdate.CirculatingSupply = updateModel.CirculatingSupply;
                entityToUpdate.CurrentPrice = updateModel.Price;
                entityToUpdate.LastUpdated = updateModel.LastUpdated;
                entityToUpdate.MarketCap = updateModel.MarketCap;
                entityToUpdate.MaxSupply = updateModel.MaxSupply;
                entityToUpdate.Name = updateModel.Name;
                entityToUpdate.PercentChange1h = updateModel.PercentChange1h;
                entityToUpdate.PercentChange24h = updateModel.PercentChange24h;
                entityToUpdate.PercentChange7d = updateModel.PercentChange7d;
                entityToUpdate.Rank = updateModel.CMCRank;
                entityToUpdate.Symbol = updateModel.Symbol;
                entityToUpdate.TotalSupply = updateModel.TotalSupply;
                entityToUpdate.Volume24h = updateModel.Volume24h;
            }
        }

        private async Task CreateEntities(IList<AddOrUpdateModel> nonExistingModels)
        {
            var entitiesToCreate = new List<Cryptocurrency>();

            foreach (var newEntityModel in nonExistingModels)
            {
                entitiesToCreate.Add(new Cryptocurrency
                {
                    CirculatingSupply = newEntityModel.CirculatingSupply,
                    CurrentPrice = newEntityModel.Price,
                    ExternalId = newEntityModel.Id,
                    LastUpdated = newEntityModel.LastUpdated,
                    MarketCap = newEntityModel.MarketCap,
                    MaxSupply = newEntityModel.MaxSupply,
                    Name = newEntityModel.Name,
                    PercentChange1h = newEntityModel.PercentChange1h,
                    PercentChange24h = newEntityModel.PercentChange24h,
                    PercentChange7d = newEntityModel.PercentChange7d,
                    Rank = newEntityModel.CMCRank,
                    Symbol = newEntityModel.Symbol,
                    TotalSupply = newEntityModel.TotalSupply,
                    Volume24h = newEntityModel.Volume24h
                });
            }

            await _dbContext.Cryptocurrencies.AddRangeAsync(entitiesToCreate).ConfigureAwait(false);
        }
    }
}
