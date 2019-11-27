using Microsoft.Extensions.Configuration;
using System;

namespace CryptoCurrencyBrowser.DataJob.Jobs
{
    public class CoinMarketCapClientConfiguration
    {
        public string ApiKey { get; private set; }
        public string ApiKeyHeaderName { get; private set; }
        public string AcceptedMediaType { get; private set; }
        public string BaseUrl { get; private set; }

        private static readonly object lockObject = new object();
        private static volatile CoinMarketCapClientConfiguration _instance;

        private CoinMarketCapClientConfiguration(string apiKey, string apiKeyHeaderName, string acceptedMediaType, string baseUrl)
        {
            ApiKey = apiKey;
            ApiKeyHeaderName = apiKeyHeaderName;
            AcceptedMediaType = acceptedMediaType;
            BaseUrl = baseUrl;
        }

        public static CoinMarketCapClientConfiguration GetInstance(IConfiguration configuration)
        {
            if (_instance != null)
            {
                return _instance;
            }

            lock (lockObject)
            {
                if (_instance == null)
                {
                    string apiKey = configuration["CoinMarketCap:Api:ApiKey"] ?? throw new ArgumentNullException($"{nameof(ApiKey)} is null");
                    string apiKeyHeaderName = configuration["CoinMarketCap:Api:ApiKeyHeaderName"]
                        ?? throw new ArgumentNullException($"{nameof(ApiKeyHeaderName)} is null");
                    string acceptedMediaType = configuration["CoinMarketCap:Api:AcceptedMediaType"] ?? throw new ArgumentNullException($"{nameof(AcceptedMediaType)} is null");
                    string baseUrl = configuration["CoinMarketCap:Api:BaseUrl"] ?? throw new ArgumentNullException($"{nameof(BaseUrl)} is null");

                    _instance = new CoinMarketCapClientConfiguration(apiKey, apiKeyHeaderName, acceptedMediaType, baseUrl);
                }
            }            

            return _instance;
        }

    }
}
