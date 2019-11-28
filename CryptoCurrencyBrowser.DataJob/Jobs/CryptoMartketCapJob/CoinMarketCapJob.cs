using CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using CryptoCurrencyBrowser.DataJob.Jobs.CryptoMartketCapJob.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob.Jobs.CryptoMarketCapJob
{
    public class CoinMarketCapJob : IJob
    {
        private readonly ILogger<CoinMarketCapJob> _logger;
        private readonly IClient _client;
        private readonly IConfiguration _configuration;
        private readonly IAddOrUpdateService _addOrUpdateService;
        private readonly ICryptoCurrencyMapperService _cryptoCurrencyMapperService;
        private CoinMarketCapClientConfiguration _coinMarketCapClientConfiguration;

        public CoinMarketCapJob(ILogger<CoinMarketCapJob> logger, IConfiguration configuration,
            IClient client,
            IAddOrUpdateService addOrUpdateService,
            ICryptoCurrencyMapperService cryptoCurrencyMapperService)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;
            _addOrUpdateService = addOrUpdateService;
            _cryptoCurrencyMapperService = cryptoCurrencyMapperService;
        }

        public async Task DoWork()
        {
            _logger.LogInformation($"Starting: {nameof(CoinMarketCapJob)}");

            var isConfigureSuccess = Configure();

            if (isConfigureSuccess)
            {
                while (true)
                {
                    var response = await GetCoinMarketCapDataAsync()
                        .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseModel = await response.Content.ReadAsStringAsync();
                        var addOrUpdateModel = _cryptoCurrencyMapperService.MapToAddOrUpdateModel(responseModel);
                        await _addOrUpdateService.AddOrUpdateCryptocurrencies(addOrUpdateModel);
                        _logger.LogInformation("Databse updated");
                    }

                    await Task.Delay(TimeSpan.FromHours(1))
                        .ConfigureAwait(false);
                }
            }
        }

        private bool Configure()
        {
            var isConfigureSuccess = true;

            _logger.LogInformation($"Configuring: {nameof(CoinMarketCapJob)}");

            try
            {
                _coinMarketCapClientConfiguration = CoinMarketCapClientConfiguration.GetInstance(_configuration);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                isConfigureSuccess = false;
                throw;
            }

            return isConfigureSuccess;
        }

        private async Task<HttpResponseMessage> GetCoinMarketCapDataAsync()
        {
            _logger.LogInformation("Get new Coin Market Cap data");

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_configuration["CoinMarketCap:Api:BaseUrl"]}/cryptocurrency/listings/latest?start=1&limit=5"),
                Method = HttpMethod.Get
            };

            request.Headers.Add(_coinMarketCapClientConfiguration.ApiKeyHeaderName, _coinMarketCapClientConfiguration.ApiKey);
            request.Headers.Add("Accepts", _coinMarketCapClientConfiguration.AcceptedMediaType);

            return await _client.SendAsync(request).ConfigureAwait(false);
        }
    }
}
