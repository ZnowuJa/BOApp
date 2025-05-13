using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MissingBankTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level3Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level4Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level5Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationSapNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeSapCostCenterVm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReasons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankTransferTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SplitPayment = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceMappings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankTransferMapping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormVersion = table.Column<int>(type: "int", nullable: false),
                    FormType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statuses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransfers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransfers");
        }
    }
}
