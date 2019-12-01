export interface ICryptocurrencyDetails {
    id: number;
    name: string;
    symbol: string;
    rank: number;
    circulatingsSupply: number;
    totalSupply: number;
    maxSupply: number;
    currentPrice: number;
    volume24h: number;
    marketCap: number;
    percentChange1h: number;
    percentChange24h: number;
    percentcChange7d: number;
    lastUpdated: Date;
}
