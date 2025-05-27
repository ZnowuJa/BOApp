using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ScrappingandSaleForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ITSaleFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ITScrappingFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleFormId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScrappingFormId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScrappingReason",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ITSaleForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorId = table.Column<int>(type: "int", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statuses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITSaleForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITSaleForms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ITSaleForms_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITScrapingForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorId = table.Column<int>(type: "int", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statuses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITScrapingForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITScrapingForms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_ITSaleFormId",
                table: "FormFiles",
                column: "ITSaleFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SaleFormId",
                table: "Assets",
                column: "SaleFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ITSaleForms_CompanyId",
                table: "ITSaleForms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ITSaleForms_EmployeeId",
                table: "ITSaleForms",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ITScrapingForms_CompanyId",
                table: "ITScrapingForms",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITSaleForms_SaleFormId",
                table: "Assets",
                column: "SaleFormId",
                principalTable: "ITSaleForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITScrapingForms_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId",
                principalTable: "ITScrapingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITSaleForms_ITSaleFormId",
                table: "FormFiles",
                column: "ITSaleFormId",
                principalTable: "ITSaleForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITScrapingForms_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId",
                principalTable: "ITScrapingForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITSaleForms_SaleFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITScrapingForms_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITSaleForms_ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITScrapingForms_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropTable(
                name: "ITSaleForms");

            migrationBuilder.DropTable(
                name: "ITScrapingForms");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SaleFormId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropColumn(
                name: "ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropColumn(
                name: "SaleFormId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ScrappingReason",
                table: "Assets");
        }
    }
}
