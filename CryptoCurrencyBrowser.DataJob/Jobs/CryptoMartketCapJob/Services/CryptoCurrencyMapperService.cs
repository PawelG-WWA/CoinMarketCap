using CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate;
using CryptoCurrencyBrowser.DataJob.Jobs.CryptoMarketCapJob.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrencyBrowser.DataJob.Jobs.CryptoMartketCapJob.Services
{
    public interface ICryptoCurrencyMapperService
    {
        IList<AddOrUpdateModel> MapToAddOrUpdateModel(string dataToMap);
    }

    public class CryptoCurrencyMapperService : ICryptoCurrencyMapperService
    {
        public IList<AddOrUpdateModel> MapToAddOrUpdateModel(string dataToMap)
        {
            var responseData = JObject.Parse(dataToMap);

            var addOrUpdateModel = new List<AddOrUpdateModel>();

            if (responseData != null)
            {
                var data = responseData["data"].Children().ToList();
                foreach (var dataEntry in data)
                {
                    var cryptocurrency = dataEntry.ToObject<CryptoCurrencyDto>(new JsonSerializer());

                    addOrUpdateModel.Add(new AddOrUpdateModel
                    {
                        CirculatingSupply = cryptocurrency.CirculatingSupply,
                        CMCRank = cryptocurrency.CMCRank,
                        Id = cryptocurrency.Id,
                        LastUpdated = cryptocurrency.Quote.USD.LastUpdated,
                        MarketCap = cryptocurrency.Quote.USD.MarketCap,
                        MaxSupply = cryptocurrency.MaxSupply,
                        Name = cryptocurrency.Name,
                        PercentChange1h = cryptocurrency.Quote.USD.PercentChange1h,
                        PercentChange24h = cryptocurrency.Quote.USD.PercentChange24h,
                        PercentChange7d = cryptocurrency.Quote.USD.PercentChange7d,
                        Price = cryptocurrency.Quote.USD.Price,
                        Symbol = cryptocurrency.Symbol,
                        TotalSupply = cryptocurrency.TotalSupply,
                        Volume24h = cryptocurrency.Quote.USD.Volume24h
                    });
                }
            }

            return addOrUpdateModel;
        }
    }
}
