using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangesinITSaleForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITSaleForms_Companies_CompanyId",
                table: "ITSaleForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ITSaleForms_Employees_EmployeeId",
                table: "ITSaleForms");

            migrationBuilder.DropIndex(
                name: "IX_ITSaleForms_CompanyId",
                table: "ITSaleForms");

            migrationBuilder.DropIndex(
                name: "IX_ITSaleForms_EmployeeId",
                table: "ITSaleForms");

            migrationBuilder.AddColumn<string>(
                name: "AssetIds",
                table: "ITSaleForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "ITSaleForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "ITSaleForms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetIds",
                table: "ITSaleForms");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "ITSaleForms");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "ITSaleForms");

            migrationBuilder.CreateIndex(
                name: "IX_ITSaleForms_CompanyId",
                table: "ITSaleForms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ITSaleForms_EmployeeId",
                table: "ITSaleForms",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITSaleForms_Companies_CompanyId",
                table: "ITSaleForms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ITSaleForms_Employees_EmployeeId",
                table: "ITSaleForms",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
