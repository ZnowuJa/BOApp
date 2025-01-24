using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixCurrencyExchangeRatepropertyindomainlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyExchamngeRate",
                table: "BusinessTravels",
                newName: "CurrencyExchangeRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrencyExchangeRate",
                table: "BusinessTravels",
                newName: "CurrencyExchamngeRate");
        }
    }
}
