using Autofac;
using CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.DI.AutofacExtensions.Builder;
using CryptoCurrencyBrowser.Domain.Entities;
using CryptoCurrencyBrowser.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoCurrencyBrowser.Tests.Application.Services.Tests
{
    [TestFixture]
    public class AddOrUpdateServiceTests
    {
        private ILogger<AddOrUpdateService> _addOrUpdateServiceLogger;

        private IContainer _container;

        private DbContextOptions<CryptocurrencyBrowserDbContext> _dbContextOptions;

        private ICryptocurrencyBrowserDbContext _dbContext;

        private IAddOrUpdateService _addOrUpdateService;

        [SetUp]
        public void SetUp()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.AddLogModule();
            _container = containerBuilder.Build();

            using (var scope = _container.BeginLifetimeScope())
            {
                _addOrUpdateServiceLogger = scope.Resolve<ILogger<AddOrUpdateService>>();
            }

            var _dbContextOptions = new DbContextOptionsBuilder<CryptocurrencyBrowserDbContext>()
                .UseInMemoryDatabase("CryptoMarketCapDB")
                .Options;

            _dbContext = new CryptocurrencyBrowserDbContext(_dbContextOptions);

            _addOrUpdateService = new AddOrUpdateService(_dbContext, _addOrUpdateServiceLogger);
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.TestCases))]
        public void Should_Create_Or_Update_Data_When_Needed(TestCaseModel testCaseModel)
        {
            // Arrange & Act
            _addOrUpdateService.AddOrUpdateCryptocurrencies(testCaseModel.AddOrUpdateModels);
            var currentResult = _dbContext.Cryptocurrencies.ToListAsync()
                .GetAwaiter()
                .GetResult();

            // Assert
            Assert.AreEqual(testCaseModel.ExpectedResult.Count, currentResult.Count);
        }

        private class TestData
        {
            private static AddOrUpdateModel first = new AddOrUpdateModel
            {
                CirculatingSupply = 1,
                CMCRank = 1,
                Id = 1,
                LastUpdated = new DateTime(2019, 1, 1),
                MarketCap = 1,
                MaxSupply = 2,
                Name = "First",
                PercentChange1h = 1,
                PercentChange24h = 24,
                PercentChange7d = 168,
                Price = 100,
                Symbol = "F",
                TotalSupply = 100,
                Volume24h = 1000
            };

            private static AddOrUpdateModel second = new AddOrUpdateModel
            {
                CirculatingSupply = 1,
                CMCRank = 1,
                Id = 2,
                LastUpdated = new DateTime(2019, 1, 1),
                MarketCap = 1,
                MaxSupply = 2,
                Name = "Second",
                PercentChange1h = 1,
                PercentChange24h = 24,
                PercentChange7d = 168,
                Price = 100,
                Symbol = "S",
                TotalSupply = 100,
                Volume24h = 1000
            };

            private static AddOrUpdateModel firstUpdated = new AddOrUpdateModel
            {
                CirculatingSupply = 1,
                CMCRank = 1,
                Id = 1,
                LastUpdated = new DateTime(2019, 1, 1),
                MarketCap = 1,
                MaxSupply = 2,
                Name = "First",
                PercentChange1h = 1,
                PercentChange24h = 24,
                PercentChange7d = 168,
                Price = 200,
                Symbol = "F",
                TotalSupply = 100,
                Volume24h = 1000
            };

            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new TestCaseModel
                    {
                        AddOrUpdateModels = new List<AddOrUpdateModel> { first },
                        ExpectedResult = new ExpectedResultModel
                        {
                            Count = 1,
                            FirstPrice = 100
                        }
                    })
                    .SetName("Should Create Data");

                    yield return new TestCaseData(new TestCaseModel
                    {
                        AddOrUpdateModels = new List<AddOrUpdateModel> { firstUpdated, second },
                        ExpectedResult = new ExpectedResultModel
                        {
                            Count = 2,
                            FirstPrice = 200
                        }
                    }).SetName("Should Update and Create Data");
                }
            }
        }

        public class TestCaseModel
        {
            public List<AddOrUpdateModel> AddOrUpdateModels { get; set; }
            public ExpectedResultModel ExpectedResult { get; set; }
        }

        public class ExpectedResultModel
        {
            public decimal FirstPrice { get; set; }
            public int Count { get; set; }
        }
    }
}