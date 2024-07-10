using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalfieldsinDeferralPaymentForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPrefix",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OperationArea",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "NumberPrefix",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "OperationArea",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "DeferralPayments");
        }
    }
}
