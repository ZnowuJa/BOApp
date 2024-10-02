using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OnboardingForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OnboardingFormId",
                table: "Instructions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnboardingFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OnboardingForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: true),
                    FirstRun = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OnboardingForms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_OnboardingFormId",
                table: "Instructions",
                column: "OnboardingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_OnboardingFormId",
                table: "FormFiles",
                column: "OnboardingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_OnboardingForms_OnboardingFormId",
                table: "FormFiles",
                column: "OnboardingFormId",
                principalTable: "OnboardingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_OnboardingForms_OnboardingFormId",
                table: "Instructions",
                column: "OnboardingFormId",
                principalTable: "OnboardingForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_OnboardingForms_OnboardingFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_OnboardingForms_OnboardingFormId",
                table: "Instructions");

            migrationBuilder.DropTable(
                name: "OnboardingForms");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_OnboardingFormId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_OnboardingFormId",
                table: "FormFiles");

            migrationBuilder.DropColumn(
                name: "OnboardingFormId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "OnboardingFormId",
                table: "FormFiles");
        }
    }
}
