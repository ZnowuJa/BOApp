using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class TestForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeferralPayments_WorkflowTemplates_WorkflowTemplateId",
                table: "DeferralPayments");

            migrationBuilder.DropIndex(
                name: "IX_DeferralPayments_WorkflowTemplateId",
                table: "DeferralPayments");

            migrationBuilder.CreateTable(
                name: "TestForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KontrahentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontrahentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontrahentPrzelew = table.Column<bool>(type: "bit", nullable: false),
                    Przelew = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requested = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_TestForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestForms");

            migrationBuilder.CreateIndex(
                name: "IX_DeferralPayments_WorkflowTemplateId",
                table: "DeferralPayments",
                column: "WorkflowTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeferralPayments_WorkflowTemplates_WorkflowTemplateId",
                table: "DeferralPayments",
                column: "WorkflowTemplateId",
                principalTable: "WorkflowTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
