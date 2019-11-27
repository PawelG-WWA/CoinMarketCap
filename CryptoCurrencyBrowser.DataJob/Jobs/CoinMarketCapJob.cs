using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob.Jobs
{
    public class CoinMarketCapJob : IJob
    {
        private readonly ILogger<CoinMarketCapJob> _logger;
        private readonly IClient _client;
        private readonly IConfiguration _configuration;
        private CoinMarketCapClientConfiguration _coinMarketCapClientConfiguration;

        public CoinMarketCapJob(ILogger<CoinMarketCapJob> logger, IConfiguration configuration, IClient client)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;
        }

        public async Task DoWork()
        {
            _logger.LogInformation($"Starting: {nameof(CoinMarketCapJob)}");

            var isConfigureSuccess = Configure();

            if (isConfigureSuccess)
            {
                while (true)
                {
                    await GetCoinMarketCapDataAsync()
                        .ConfigureAwait(false);

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

        private async Task GetCoinMarketCapDataAsync()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?start=1&limit=100&convert=USD"),
                Method = HttpMethod.Get,
            };

            request.Headers.Add(_coinMarketCapClientConfiguration.ApiKeyHeaderName, _coinMarketCapClientConfiguration.ApiKey);
            request.Headers.Add("Accepts", _coinMarketCapClientConfiguration.AcceptedMediaType);

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            var actualResponse = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(actualResponse);
        }
    }
}
