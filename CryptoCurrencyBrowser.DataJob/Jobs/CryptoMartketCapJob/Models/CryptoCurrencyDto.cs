using Newtonsoft.Json;
using System;

namespace CryptoCurrencyBrowser.DataJob.Jobs.CryptoMarketCapJob.Models
{
    public class CryptoCurrencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        [JsonProperty("cmc_rank")]
        public int CMCRank { get; set; }
        [JsonProperty("circulating_supply")]
        public long CirculatingSupply { get; set; }
        [JsonProperty("total_supply")]
        public long TotalSupply { get; set; }
        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public USD USD { get; set; }
    }

    /// <summary>
    /// For the sake of simplicity, as there's in no requirement for that,
    /// I keep every price-related data in USD
    /// </summary>
    public class USD
    {
        public decimal Price { get; set; }
        [JsonProperty("volume_24h")]
        public decimal? Volume24h { get; set; }
        [JsonProperty("market_cap")]
        public decimal MarketCap { get; set; }
        [JsonProperty("percent_change_1h")]
        public double PercentChange1h { get; set; }
        [JsonProperty("percent_change_24h")]
        public double PercentChange24h { get; set; }
        [JsonProperty("percent_change_7d")]
        public double PercentChange7d { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
