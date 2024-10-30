using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangetablenameforScrappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITScrapingForms_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITScrapingForms_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ITScrapingForms_Companies_CompanyId",
                table: "ITScrapingForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ITScrapingForms",
                table: "ITScrapingForms");

            migrationBuilder.RenameTable(
                name: "ITScrapingForms",
                newName: "ITScrappingForms");

            migrationBuilder.RenameIndex(
                name: "IX_ITScrapingForms_CompanyId",
                table: "ITScrappingForms",
                newName: "IX_ITScrappingForms_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ITScrappingForms",
                table: "ITScrappingForms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITScrappingForms_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId",
                principalTable: "ITScrappingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITScrappingForms_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId",
                principalTable: "ITScrappingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ITScrappingForms_Companies_CompanyId",
                table: "ITScrappingForms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITScrappingForms_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITScrappingForms_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ITScrappingForms_Companies_CompanyId",
                table: "ITScrappingForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ITScrappingForms",
                table: "ITScrappingForms");

            migrationBuilder.RenameTable(
                name: "ITScrappingForms",
                newName: "ITScrapingForms");

            migrationBuilder.RenameIndex(
                name: "IX_ITScrappingForms_CompanyId",
                table: "ITScrapingForms",
                newName: "IX_ITScrapingForms_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ITScrapingForms",
                table: "ITScrapingForms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITScrapingForms_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId",
                principalTable: "ITScrapingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITScrapingForms_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId",
                principalTable: "ITScrapingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ITScrapingForms_Companies_CompanyId",
                table: "ITScrapingForms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
