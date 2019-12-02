using Autofac;
using CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate;
using CryptoCurrencyBrowser.Application.Cryptocurrencies.GetCryptocurrencyDetails;
using CryptoCurrencyBrowser.DataJob;
using CryptoCurrencyBrowser.DataJob.Jobs.CryptoMarketCapJob;
using CryptoCurrencyBrowser.DataJob.Jobs.CryptoMartketCapJob.Services;
using CryptoCurrencyBrowser.DI.AutofacExtensions.Builder;
using CryptoCurrencyBrowser.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Net.Http;

namespace CryptoCurrencyBrowser.Tests.DataJob.Tests.Jobs
{
    [TestFixture]
    public class CryptoMarketCapJobTests
    {
        private DbContextOptions<CryptocurrencyBrowserDbContext> _dbContextOptions;

        private IConfiguration _configuration;

        private ILogger<CoinMarketCapJob> _cryptoMarketCapJobLogger;

        private ILogger<AddOrUpdateService> _addOrUpdateServiceLogger;

        private IContainer _container;

        private Mock<IClient> _clientMock;

        private IAddOrUpdateService _addOrUpdateService;

        private IGetCryptocurrencyDetailsService _getCryptocurrencyDetailsService;

        [SetUp]
        public void SetUp()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddLogModule();
            _container = containerBuilder.Build();

            var _dbContextOptions = new DbContextOptionsBuilder<CryptocurrencyBrowserDbContext>()
                .UseInMemoryDatabase("CryptoMarketCapDB")
                .Options;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            using (var scope = _container.BeginLifetimeScope())
            {
                _cryptoMarketCapJobLogger = scope.Resolve<ILogger<CoinMarketCapJob>>();
                _addOrUpdateServiceLogger = scope.Resolve<ILogger<AddOrUpdateService>>();
            }

            _configuration = builder.Build();

            var dbContext = new CryptocurrencyBrowserDbContext(_dbContextOptions);

            _addOrUpdateService = new AddOrUpdateService(dbContext, _addOrUpdateServiceLogger);

            _getCryptocurrencyDetailsService = new GetCryptocurrencyDetailsService(dbContext);

            _clientMock = new Mock<IClient>();
            _clientMock
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    Content = new StringContent(@"{
                        'status': {
                            'timestamp': '2019-12-02T22:00:41.391Z',
                            'error_code': 0,
                            'error_message': null,
                            'elapsed': 11,
                            'credit_count': 1,
                            'notice': null
                        },
                        'data': [
                            {
                                'id': 1,
                                'name': 'Bitcoin',
                                'symbol': 'BTC',
                                'slug': 'bitcoin',
                                'num_market_pairs': 7694,
                                'date_added': '2013-04-28T00:00:00.000Z',
                                'tags': [
                                    'mineable'
                                ],
                                'max_supply': 21000000,
                                'circulating_supply': 18079550,
                                'total_supply': 18079550,
                                'platform': null,
                                'cmc_rank': 1,
                                'last_updated': '2019-12-02T21:59:33.000Z',
                                'quote': {
                                    'USD': {
                                        'price': 7320.7138507,
                                        'volume_24h': 17354104490.4711,
                                        'percent_change_1h': -0.0248255,
                                        'percent_change_24h': -0.956814,
                                        'percent_change_7d': 1.67993,
                                        'market_cap': 132355212099.42319,
                                        'last_updated': '2019-12-02T21:59:33.000Z'
                                    }
                                }
                            }]}")
                });


        }

        [Test]
        public void Should_Save_CryptoMarketCap_Data_To_The_Database()
        {
            // Arrange
            var coinMarketCapJob = new CoinMarketCapJob(_cryptoMarketCapJobLogger,
                _configuration,
                _clientMock.Object,
                _addOrUpdateService,
                new CryptoCurrencyMapperService());

            // Act
            coinMarketCapJob.DoWork(false)
                .GetAwaiter()
                .GetResult();

            var details = _getCryptocurrencyDetailsService.GetDetailsById(1)
                .GetAwaiter()
                .GetResult();

            // Assert
            _clientMock.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
            Assert.IsNotNull(details);
            Assert.AreEqual("Bitcoin", details.Name);
            Assert.AreEqual(-0.956814, details.PercentChange24h);
        }
    }
}
