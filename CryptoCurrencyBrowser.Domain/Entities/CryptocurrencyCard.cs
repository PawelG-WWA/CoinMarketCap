namespace CryptoCurrencyBrowser.Domain.Entities
{
    public class CryptocurrencyCard
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Volume24h { get; set; }
        public decimal MarketCap { get; set; }
        public double PercentChange24h { get; set; }
    }
}
