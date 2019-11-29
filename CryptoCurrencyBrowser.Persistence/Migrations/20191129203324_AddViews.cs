using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoCurrencyBrowser.Persistence.Migrations
{
    public partial class AddViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW View_CryptoCurrencyCards AS
             SELECT [Id]
                ,[ExternalId]
                ,[Name]
                ,[Symbol]
                ,[Rank]
                ,[CurrentPrice]
                ,[Volume24h]
                ,[MarketCap]
                ,[PercentChange24h]
            FROM dbo.Cryptocurrencies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS dbo.View_CryptoCurrencyCards");
        }
    }
}
