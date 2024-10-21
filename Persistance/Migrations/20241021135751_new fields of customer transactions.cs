using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class newfieldsofcustomertransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CC",
                table: "DeferralPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Faktdoc_Id",
                table: "DeferralPayments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Numer_Fk",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_Firma",
                table: "DeferralPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CC",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "Faktdoc_Id",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "Numer_Fk",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "is_Firma",
                table: "DeferralPayments");
        }
    }
}
