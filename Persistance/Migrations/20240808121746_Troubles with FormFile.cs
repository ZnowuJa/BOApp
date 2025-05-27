using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class TroubleswithFormFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormFiles",
                table: "TestForms");

            migrationBuilder.AddColumn<int>(
                name: "TestFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_TestFormId",
                table: "FormFiles",
                column: "TestFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_TestForms_TestFormId",
                table: "FormFiles",
                column: "TestFormId",
                principalTable: "TestForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_TestForms_TestFormId",
                table: "FormFiles");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_TestFormId",
                table: "FormFiles");

            migrationBuilder.DropColumn(
                name: "TestFormId",
                table: "FormFiles");

            migrationBuilder.AddColumn<string>(
                name: "FormFiles",
                table: "TestForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
