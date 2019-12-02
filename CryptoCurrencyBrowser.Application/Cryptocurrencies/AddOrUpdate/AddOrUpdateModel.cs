using System;

namespace CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate
{
    public class AddOrUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int CMCRank { get; set; }
        public long CirculatingSupply { get; set; }
        public long TotalSupply { get; set; }
        public long? MaxSupply { get; set; }
        public decimal Price { get; set; }
        public decimal? Volume24h { get; set; }
        public decimal MarketCap { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
