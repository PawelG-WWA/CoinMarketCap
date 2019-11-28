using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoCurrencyBrowser.Persistence.Migrations
{
    public partial class MaxSupplyNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PercentCHange7d",
                table: "Cryptocurrencies",
                newName: "PercentChange7d");

            migrationBuilder.AlterColumn<long>(
                name: "MaxSupply",
                table: "Cryptocurrencies",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PercentChange7d",
                table: "Cryptocurrencies",
                newName: "PercentCHange7d");

            migrationBuilder.AlterColumn<long>(
                name: "MaxSupply",
                table: "Cryptocurrencies",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
