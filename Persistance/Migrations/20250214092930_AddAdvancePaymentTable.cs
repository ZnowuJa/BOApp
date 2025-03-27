using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvancePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvancePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationSapNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormCostCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormCostLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level3Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level4Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level5Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL1_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL2_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL3_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL3_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL4_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL4_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL5_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL5_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancePaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdvancePaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvancePaymentCash = table.Column<bool>(type: "bit", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvancePaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CashPayoutNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayoutCashierEmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BTMappingAdvancePayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BTMappingPayout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CashPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_AdvancePayments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvancePayments");
        }
    }
}
