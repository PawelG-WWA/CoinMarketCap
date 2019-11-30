export interface ICryptocurrencyCard {
    id: number;
    externalId: number;
    name: string;
    symbol: string;
    rank: number;
    currentPrice: number;
    volume24h: number;
    marketCap: number;
    percentChange24h: number;
}
