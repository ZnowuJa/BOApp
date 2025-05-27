using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BusinessTravel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "WorkflowTemplates",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Warehouses",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Vendors",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "VATRates",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Units",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "States",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Positions",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Parts",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Organisations",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "InvoiceItems",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "EmployeeTypes",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Currencies",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Countries",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "CompanyTypes",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Companies",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "CategoryTypes",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Categories",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "AspNetUserTokens",
            //    newName: "Name");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "AspNetRoles",
            //    newName: "Name");

            migrationBuilder.CreateTable(
                name: "BusinessTravels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationCountryCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateVehicle = table.Column<bool>(type: "bit", nullable: false),
                    PrivateVehicleEngineSize = table.Column<int>(type: "int", nullable: false),
                    PrivateVehicleMilage = table.Column<int>(type: "int", nullable: false),
                    PrivateVehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyVehicle = table.Column<bool>(type: "bit", nullable: false),
                    CompanyVehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicTransport = table.Column<bool>(type: "bit", nullable: false),
                    PublicTransportPaid = table.Column<bool>(type: "bit", nullable: false),
                    OrganisationSapNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approvals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level1Approvers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level2Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level3Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level4Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level5Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level6Approvers = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    LVL6_EnovaEmpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LVL6_EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvancePayment = table.Column<bool>(type: "bit", nullable: false),
                    AdvancePaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AdvancePaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvancePaymentCash = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvancePaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CashPayoutNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CashReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayoutCashierEmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptCashierEmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyExchamngeRate = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    CurrencyExchangeRateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConveyanceTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accommodations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalTravels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BTMappingAdvancePayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BTMappingPayout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowancePL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AllowanceNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SumAllowancePL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SumAllowanceNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DeductionMealsPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DeductionMealsNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AccomodationAllowanceSumPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AccomodationAllowanceSumNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SumLocalTravelAllowancePL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SumLocalTravelAllowanceNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SumPrivateVehicleAllowance = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CashPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalBillsPL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalBillsNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalAllowancePL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalAllowanceNotPL = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPayOut = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statuses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTravels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTravels");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "WorkflowTemplates",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Warehouses",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Vendors",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "VATRates",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Units",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "States",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Positions",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Parts",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Organisations",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "InvoiceItems",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "EmployeeTypes",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Currencies",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Countries",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "CompanyTypes",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Companies",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "CategoryTypes",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Categories",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "AspNetUserTokens",
            //    newName: "Title");

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "AspNetRoles",
            //    newName: "Title");
        }
    }
}
