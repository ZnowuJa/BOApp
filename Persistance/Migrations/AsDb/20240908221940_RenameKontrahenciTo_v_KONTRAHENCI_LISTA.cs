using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.AsDb
{
    /// <inheritdoc />
    public partial class RenameKontrahenciTo_v_KONTRAHENCI_LISTA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
            name: "Kontrahenci",
            newName: "v_KONTRAHENCI_LISTA");

            //migrationBuilder.CreateTable(
            //    name: "v_KONTRAHENCI_LISTA",
            //    columns: table => new
            //    {
            //        KONTRAHENT_ID = table.Column<long>(type: "bigint", nullable: false),
            //        NAZWA = table.Column<string>(type: "varchar(8000)", unicode: false, maxLength: 8000, nullable: true, collation: "Polish_CI_AS"),
            //        NIP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, collation: "Polish_CI_AS"),
            //        PRZELEW = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
            name: "v_KONTRAHENCI_LISTA",
            newName: "Kontrahenci");
            //migrationBuilder.DropTable(
            //    name: "v_KONTRAHENCI_LISTA");
        }
    }
}
