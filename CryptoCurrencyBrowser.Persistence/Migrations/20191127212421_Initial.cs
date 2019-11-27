using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoCurrencyBrowser.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    CirculatingSupply = table.Column<long>(nullable: false),
                    TotalSupply = table.Column<long>(nullable: false),
                    MaxSupply = table.Column<long>(nullable: false),
                    CurrentPrice = table.Column<decimal>(nullable: false),
                    Volume24h = table.Column<decimal>(nullable: false),
                    MarketCap = table.Column<decimal>(nullable: false),
                    PercentChange1h = table.Column<double>(nullable: false),
                    PercentChange24h = table.Column<double>(nullable: false),
                    PercentCHange7d = table.Column<double>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptocurrencies");
        }
    }
}
