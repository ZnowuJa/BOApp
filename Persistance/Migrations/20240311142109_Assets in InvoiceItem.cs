using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AssetsinInvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ItemsGenerated",
            //    table: "Invoices",
            //    newName: "ItemsGenerated");

            //migrationBuilder.RenameColumn(
            //    name: "ItemsGenerated",
            //    table: "InvoiceItems",
            //    newName: "ItemsGenerated");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceItemId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceItemId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "ItemsGenerated",
                table: "Invoices",
                newName: "ItemsGenerated");

            migrationBuilder.RenameColumn(
                name: "ItemsGenerated",
                table: "InvoiceItems",
                newName: "ItemsGenerated");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
