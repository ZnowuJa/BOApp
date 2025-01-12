using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccomodationAllowance",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "AllowanceFirstDay12H",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "AllowanceFirstDay8H",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "AllowanceNextDay12H",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "AllowanceNextDay8H",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "BreakfastReduction",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DinnerReduction",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LunchReduction",
                table: "Countries");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxHotelCost",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxHotelCost",
                table: "Countries");

            migrationBuilder.AddColumn<decimal>(
                name: "LunchReduction",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AccomodationAllowance",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AllowanceFirstDay12H",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AllowanceFirstDay8H",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AllowanceNextDay12H",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AllowanceNextDay8H",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BreakfastReduction",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DinnerReduction",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
