using System;

namespace CryptoCurrencyBrowser.Domain.Entities
{
    public class Cryptocurrency
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public long CirculatingSupply { get; set; }
        public long TotalSupply { get; set; }
        public long? MaxSupply { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? Volume24h { get; set; }
        public decimal MarketCap { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
