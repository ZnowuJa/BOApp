using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangesForBusinessPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "BusinessPartners");

            migrationBuilder.RenameColumn(
                name: "LongName",
                table: "BusinessPartners",
                newName: "SAPFormType");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "BusinessPartners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultCurrency",
                table: "BusinessPartners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCurrency",
                table: "BusinessPartners");

            migrationBuilder.RenameColumn(
                name: "SAPFormType",
                table: "BusinessPartners",
                newName: "LongName");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "BusinessPartners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "BusinessPartners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
