using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptTableNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "AdvancePaymentCash",
                table: "BusinessTravels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CashPointReceipt",
                table: "BusinessTravels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiptBankAccountNumber",
                table: "BusinessTravels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiptPaymentCash",
                table: "BusinessTravels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptPaymentCurrency",
                table: "BusinessTravels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashPointReceipt",
                table: "BusinessTravels");

            migrationBuilder.DropColumn(
                name: "ReceiptBankAccountNumber",
                table: "BusinessTravels");

            migrationBuilder.DropColumn(
                name: "ReceiptPaymentCash",
                table: "BusinessTravels");

            migrationBuilder.DropColumn(
                name: "ReceiptPaymentCurrency",
                table: "BusinessTravels");

            migrationBuilder.AlterColumn<bool>(
                name: "AdvancePaymentCash",
                table: "BusinessTravels",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
