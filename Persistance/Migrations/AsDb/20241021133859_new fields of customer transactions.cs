using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.AsDb
{
    /// <inheritdoc />
    public partial class newfieldsofcustomertransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME = 'v_KONTRAHENCI_LISTA' AND COLUMN_NAME = 'CC')
                BEGIN
                    ALTER TABLE v_KONTRAHENCI_LISTA ADD CC int NOT NULL DEFAULT 0
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME = 'v_KONTRAHENCI_LISTA' AND COLUMN_NAME = 'Faktdoc_Id')
                BEGIN
                    ALTER TABLE v_KONTRAHENCI_LISTA ADD Faktdoc_Id bigint NOT NULL DEFAULT 0
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME = 'v_KONTRAHENCI_LISTA' AND COLUMN_NAME = 'Numer_Fk')
                BEGIN
                    ALTER TABLE v_KONTRAHENCI_LISTA ADD Numer_Fk nvarchar(max) NOT NULL DEFAULT ''
                END");

            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME = 'v_KONTRAHENCI_LISTA' AND COLUMN_NAME = 'is_Firma')
                BEGIN
                    ALTER TABLE v_KONTRAHENCI_LISTA ADD is_Firma bit NOT NULL DEFAULT 0
                END");


            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CC",
                table: "v_KONTRAHENCI_LISTA");

            migrationBuilder.DropColumn(
                name: "Faktdoc_Id",
                table: "v_KONTRAHENCI_LISTA");

            migrationBuilder.DropColumn(
                name: "Numer_Fk",
                table: "v_KONTRAHENCI_LISTA");

            migrationBuilder.DropColumn(
                name: "is_Firma",
                table: "v_KONTRAHENCI_LISTA");
        }
    }
}
