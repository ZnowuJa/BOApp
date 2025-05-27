using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddBankTransferAndBusinessPartnerTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "BusinessPartners",
                newName: "BusinessPartnerType");

            migrationBuilder.AddColumn<string>(
                name: "BankTransferType",
                table: "BusinessPartners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankTransferType",
                table: "BusinessPartners");

            migrationBuilder.RenameColumn(
                name: "BusinessPartnerType",
                table: "BusinessPartners",
                newName: "Type");
        }
    }
}
