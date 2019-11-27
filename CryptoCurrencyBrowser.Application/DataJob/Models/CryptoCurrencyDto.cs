using Newtonsoft.Json;
using System;

namespace CryptoCurrencyBrowser.Application.DataJob.Models
{
    public class CryptoCurrencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int CMCRank { get; set; }
        public long CirculatingSupply { get; set; }
        public long TotalSupply { get; set; }
        public long MaxSupply { get; set; }
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
        public decimal Volume24h { get; set; }
        public decimal MarketCap { get; set; }
        [JsonProperty("percent_change_1h")]
        public double PercentChange1h { get; set; }
        [JsonProperty("percent_change_24h")]
        public double PercentChange24h { get; set; }
        [JsonProperty("percent_change_7d")]
        public double PercentChange7d { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
