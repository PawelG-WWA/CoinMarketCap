--EF Core assumes that sets configured with .ToView() already exist in DB and
--migration script won't generate one
--https://docs.microsoft.com/en-us/ef/core/modeling/keyless-entity-types - mapping to database objects section

CREATE VIEW View_CryptocurrencyCards AS
SELECT [Id],
    [ExternalId],
    [Name],
    [Symbol],
    [Rank],
    [CurrentPrice],
    [Volume24h],
    [MarketCap],
    [PercentChange24h]
FROM [dbo].[Cryptocurrencies]