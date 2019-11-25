using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace CryptoCurrencyBrowser.DataJob.Jobs
{
    public class CoinMarketCapJob : IJob
    {
        private readonly ILogger<CoinMarketCapJob> _logger;

        public CoinMarketCapJob(ILogger<CoinMarketCapJob> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation($"Starting: {nameof(CoinMarketCapJob)}", null);
            var dataJobCallback = new TimerCallback(DoWork);

            var timer = new Timer(dataJobCallback,
                null,
                TimeSpan.FromSeconds(0),
                TimeSpan.FromSeconds(1));
        }

        public void DoWork(object state)
        {
        }
    }
}
